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
        DefaultShopPromotionService<T> : DefaultEntityService<T, MinimumShopPromotionResource,
            MinimumShopPromotionResource, ShopPromotion, ShopPromotionDomainContext>
        where T : ShopPromotionForm
    {
        public DefaultShopPromotionService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<ShopPromotion> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .Where(sp =>
                    sp.Shop.ShopStatuses.Where(ss => ss.Status == ShopStatusOption.Approved)
                        .OrderByDescending(ss => ss.Id)
                        .FirstOrDefault().Id == sp.Shop.ShopStatuses.OrderByDescending(ss => ss.Id).FirstOrDefault().Id)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<ShopPromotion> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            Query = Query.Where(sp =>
                sp.Shop.ShopStatuses.Where(ss => ss.Status == ShopStatusOption.Approved).OrderByDescending(ss => ss.Id)
                    .FirstOrDefault().Id == sp.Shop.ShopStatuses.OrderByDescending(ss => ss.Id).FirstOrDefault().Id);

            // Filter by shop id.
            if (entityTypeParameters.GetParameter("ShopId") != null)
                Query = Query.Where(x => x.ShopId == (int) entityTypeParameters.GetParameter("ShopId"));

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override ShopPromotion MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map shop promotion and resource.
            var shopPromotion = Mapper.Map<ShopPromotion>(form);

            // Assign shop to new promotion.
            var shop = Context.Shops.SingleOrDefaultAsync(x => x.Id == form.ShopId, ct);
            // Validation requested id.
            if (shop.Result == null) throw new ShopNotFoundException();
            shopPromotion.Shop = shop.Result;

            // Assign payment method to new shop promotion if exists.
            if (form.PaymentMethodId != null)
            {
                var paymentMethod = Context.PaymentMethods.SingleOrDefaultAsync(x => x.Id == form.PaymentMethodId, ct);
                // Validation requested id.
                if (paymentMethod.Result == null) throw new PaymentMethodNotFoundException();
                shopPromotion.PaymentMethod = paymentMethod.Result;
            }

            return shopPromotion;
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if shop have duplicated size for each product group thrown error.
        /// </summary>
        protected override void ValidateAddOrUpdateRequest(T form)
        {
        }
    }
}