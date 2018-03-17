// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Exceptions;
    using Extensions;
    using Infrastructure;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Form;
    using PaginationHelper;

    public class
        DefaultShopGeolocationService<T> : DefaultEntityService<T, MinimumShopGeolocationListResource,
            MinimumShopGeolocationResource, ShopGeolocation, ShopPromotionDomainContext>
        where T : ShopGeolocationForm
    {
        public DefaultShopGeolocationService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<ShopGeolocation> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .Include(x => x.Shop)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<ShopGeolocation> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            Query.Include(x => x.Shop);

            // Filter by attribute if exists.
            if (entityTypeParameters.GetParameter("AttributeId") != null)
                Query = Query.Where(x =>
                    x.Shop.ShopAttributes.Any(att => att.Id == (int) entityTypeParameters.GetParameter("AttributeId")));

            var geolocationPoint = (GeolocationPoint) entityTypeParameters.GetParameter("GeolocationPoint");
            if (geolocationPoint == null) return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);

            // Filter by geolocation if exists.
            if (!geolocationPoint.IsEmpty())
            {
                Query = Query.Where(x => IsInRadius(geolocationPoint.Latitude, geolocationPoint.Longitude, x.Latitude,
                    x.Longitude, geolocationPoint.Radius));
            }

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override ShopGeolocation MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map shop promotion review and resource.
            var shopGeolocation = Mapper.Map<ShopGeolocation>(form);

            // Assign shop to new review.
            var shop = Context.ShopPromotions.SingleOrDefaultAsync(x => x.Id == form.ShopId, ct);
            // Validation requested id.
            if (shop.Result == null) throw new ShopNotFoundException();
            shopGeolocation.ShopId = shop.Result.Id;

            return shopGeolocation;
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if shop promotion review have duplicated size for each product group thrown error.
        /// </summary>
        protected override void ValidateAddOrUpdateRequest(T form)
        {
            if (GetCurrentAction() != CreateEntity) return;

            if (Context.ShopGeolocations.Any(x => x.ShopId == form.ShopId))
            {
                throw new DuplicateShopGeolocationException();
            }
        }

        /// <summary>
        /// Is in given radius or not.
        /// </summary>
        /// <param name="originLatitude"></param>
        /// <param name="originLongitude"></param>
        /// <param name="destinationLatitude"></param>
        /// <param name="destinationLongitude"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private bool IsInRadius(double originLatitude, double originLongitude, double destinationLatitude,
            double destinationLongitude, int radius)
        {
            return GeoCalculator.GetDistance(originLatitude, originLongitude, destinationLatitude,
                       destinationLongitude) < radius;
        }
    }
}