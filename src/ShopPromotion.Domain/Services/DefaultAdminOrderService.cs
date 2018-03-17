// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopPromotion.Domain.Extensions;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Infrastructure;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using PaginationHelper;

    public class
        DefaultOrderService<T> : DefaultEntityService<T, MinimumOrderResource, MinimumOrderResource, Order, ShopPromotionDomainContext>
        where T : BaseEntity
    {
        public DefaultOrderService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<Order> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<Order> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            // Filter by contract.
            if (entityTypeParameters.GetParameter("ShopId") != null)
            {
                Query = Query.Where(x =>
                    x.ShopPromotionBarcode.Promotion.ShopId == (int) entityTypeParameters.GetParameter("ShopId"));   
            }

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override Order MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map order and resource.
            var order = Mapper.Map<Order>(form);

            return order;
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