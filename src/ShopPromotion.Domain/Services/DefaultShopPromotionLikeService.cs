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
        DefaultShopPromotionLikeService<T> : DefaultEntityService<T, MinimumShopPromotionLikeListResource,
            MinimumShopPromotionLikeResource, ShopPromotionLike, ShopPromotionDomainContext>
        where T : ShopPromotionLikeForm
    {
        public DefaultShopPromotionLikeService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<ShopPromotionLike> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .Include(x => x.LikedBy)
                .Include(x => x.ShopPromotion)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<ShopPromotionLike> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            // Filter by promotion id.
            Query = Query.Where(x =>
                    x.ShopPromotion.Id == (int) entityTypeParameters.GetParameter("ShopPromotionId")
                        &&
                    x.Liked
                );

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override ShopPromotionLike MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map shop promotion review and resource.
            var shopPromotionLike = Mapper.Map<ShopPromotionLike>(form);
            shopPromotionLike.Liked = !shopPromotionLike.Liked;

            // Assign shop promotion to new review.
            var shopPromotion = Context.ShopPromotions.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == form.ShopPromotionId, ct);
            // Validation requested id.
            if (shopPromotion.Result == null) throw new ShopPromotionNotFoundException();
            shopPromotionLike.ShopPromotionId = shopPromotion.Result.Id;

            return shopPromotionLike;
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