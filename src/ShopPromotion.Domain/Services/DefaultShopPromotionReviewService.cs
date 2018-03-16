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
        DefaultShopPromotionReviewService<T> : DefaultEntityService<T, MinimumShopPromotionReviewListResource,
            MinimumShopPromotionReviewResource, ShopPromotionReview, ShopPromotionDomainContext>
        where T : ShopPromotionReviewForm
    {
        public DefaultShopPromotionReviewService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<ShopPromotionReview> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.ShopPromotion)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<ShopPromotionReview> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            // Filter by promotion id.
            Query = Query.Where(x =>
                x.ShopPromotion.Id == (int) entityTypeParameters.GetParameter("ShopPromotionId"));

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override ShopPromotionReview MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map shop promotion review and resource.
            var shopPromotionReview = Mapper.Map<ShopPromotionReview>(form);

            // Assign shop promotion to new review.
            var shopPromotion = Context.ShopPromotions.SingleOrDefaultAsync(x => x.Id == form.ShopPromotionId, ct);
            // Validation requested id.
            if (shopPromotion.Result == null) throw new ShopPromotionNotFoundException();
            shopPromotionReview.ShopPromotionId = shopPromotion.Result.Id;

            // Calculate new shopPromotion rating
            if (form.Rating == 0) return shopPromotionReview;
            var ratingCount = Query.Where(x => x.ShopPromotion.Id == shopPromotion.Result.Id).CountAsync(ct).Result;
            shopPromotion.Result.AverageRating =
                (shopPromotion.Result.AverageRating * ratingCount + form.Rating) / (ratingCount + 1);

            // Context.Set<ShopPromotion>().Add(shopPromotion.Result);

            return shopPromotionReview;
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if shop promotion review have duplicated size for each product group thrown error.
        /// </summary>
        protected override void ValidateAddOrUpdateRequest(T form)
        {
        }
    }
}