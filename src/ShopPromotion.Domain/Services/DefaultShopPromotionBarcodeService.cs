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
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using PaginationHelper;

    public class
        DefaultShopPromotionBarcodeService<T> : DefaultEntityService<T, MinimumShopPromotionBarcodeResource,
            MinimumShopPromotionBarcodeResource, ShopPromotionBarcode, ShopPromotionDomainContext>
        where T : ShopPromotionBarcodeForm
    {
        public DefaultShopPromotionBarcodeService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<ShopPromotionBarcode> GetElementOfTModelSequenceAsync(int id,
            CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<ShopPromotionBarcode> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            Query = Query
                .Where(x =>
                    x.Promotion.Shop.ShopStatuses.Where(ss => ss.Status == ShopStatusOption.Approved)
                        .OrderByDescending(ss => ss.Id)
                        .FirstOrDefault().Id ==
                    x.Promotion.Shop.ShopStatuses.OrderByDescending(ss => ss.Id).FirstOrDefault().Id);

            // Filter by shop id.
            if (entityTypeParameters.GetParameter("ShopId") != null)
                Query = Query.Where(x => x.Promotion.ShopId == (int) entityTypeParameters.GetParameter("ShopId"));

            // Filter by promotion id.
            if (entityTypeParameters.GetParameter("PromotionId") != null)
                Query = Query.Where(x => x.Promotion.Id == (int) entityTypeParameters.GetParameter("PromotionId"));

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override ShopPromotionBarcode MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map shop promotion and resource.
            var shopPromotionBarcode = Mapper.Map<ShopPromotionBarcode>(form);

            // Assign shop promotion to new barcode.
            var shopPromotion = Context.ShopPromotions.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == form.PromotionId, ct);
            // Validation requested id.
            if (shopPromotion.Result == null) throw new ShopPromotionNotFoundException();
            shopPromotionBarcode.PromotionId = shopPromotion.Result.Id;

            return shopPromotionBarcode;
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