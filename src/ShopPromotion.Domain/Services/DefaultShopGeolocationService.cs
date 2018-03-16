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
            // Filter by shop id.
            Query = Query.Where(x =>
                x.ShopId == (int) entityTypeParameters.GetParameter("ShopId"));

            // Filter by attribute if exists.
            if (entityTypeParameters.GetParameter("AttributeId") != null)
                Query = Query.Where(x =>
                    x.Shop.ShopAttributes.Any(att => att.Id == (int) entityTypeParameters.GetParameter("AttributeId")));

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
    }
}