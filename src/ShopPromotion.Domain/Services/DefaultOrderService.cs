// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopPromotion.Domain.Exceptions;
using ShopPromotion.Domain.Infrastructure.Models.Form;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Infrastructure;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using PaginationHelper;

    public class
        DefaultOrderService<T> : DefaultEntityService<T, MinimumOrderListResource, MinimumOrderResource, Order,
            ShopPromotionDomainContext>
        where T : OrderForm
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
        protected override IQueryable<Order> GetElementsOfTModelSequenceAsync(IEntityTypeParameters entityTypeParameters)
        {
            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override Order MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Order can not create externally from API.
            if (GetCurrentAction() == CreateEntity)
            {
                throw new NotSupportedException();
            }

            // Map order and resource.
            var order = Mapper.Map<Order>(form);

            if (!String.IsNullOrEmpty(form.PromotionBarcode))
            {
                var promotionBarcode =
                    Context.PromotionBarcodes.SingleOrDefault(pb => pb.Barcode == form.PromotionBarcode);
                if (promotionBarcode == null) throw new PromotionBarcodeNotFoundException();
                // Assign finded barcode to order
                order.ShopPromotionBarcodeId = promotionBarcode.Id;
            }

            // Assing owner of order.
            order.CustomerId = form.CreatedById;

            return order;
        }

        /// <inheritdoc />
        protected override void ValidateAddOrUpdateRequest(T form)
        {
        }
    }
}