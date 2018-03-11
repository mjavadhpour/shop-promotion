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
    using Infrastructure;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Response.Pagination;
    using PaginationHelper;

    public class
        DefaultShopPromotionService : DefaultEntityService<ShopPromotionForm, MinimumShopPromotionResource, MinimumShopPromotionResource, ShopPromotion, ShopPromotionDomainContext>
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
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override ShopPromotion MappingFromModelToTModelDestination(ShopPromotionForm form, CancellationToken ct)
        {
            // Map shop promotion and resource.
            var shopPromotion = Mapper.Map<ShopPromotion>(form);

            return shopPromotion;
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        public override async Task<IPage<MinimumShopPromotionResource>> GetEntitiesAsync(IPagingOptions pagingOptions,
            IEntityTypeParameters entityTypeParameters, CancellationToken ct)
        {
            // Filter by contract.
            if (entityTypeParameters.GetParameter("ShopId") != null)
                Query = Query.Where(x => x.ShopId == (int) entityTypeParameters.GetParameter("ShopId"));

            return await base.GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if shop have duplicated size for each product group thrown error.
        /// </summary>
        protected override void ValidateAddOrUpdateRequest(ShopPromotionForm form)
        {
        }
    }
}