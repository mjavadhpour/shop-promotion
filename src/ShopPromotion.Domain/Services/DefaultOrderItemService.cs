// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Infrastructure;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Form;
    using PaginationHelper;

    public class
        DefaultOrderItemService<T> : DefaultEntityService<T, MinimumOrderItemListResource, MinimumOrderItemResource,
            OrderItem,
            ShopPromotionDomainContext>
        where T : OrderItemForm
    {
        public DefaultOrderItemService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<OrderItem> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<OrderItem> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            // Filter by order if exists.
            if (entityTypeParameters.GetParameter("OrderId") == null)
                return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);

            var parameter = (string) entityTypeParameters.GetParameter("OrderId");
            if (parameter == "Current")
            {
                Query = Query.Where(x =>
                    x.OrderId == Context.Orders.Where(o => o.State == OrderStateOptions.Cart).Select(o => o.Id)
                        .SingleOrDefault());
            }
            else
            {
                Query = Query.Where(x => x.OrderId == Int32.Parse(parameter));                    
            }

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override OrderItem MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map orderItem and resource.
            var orderItem = Mapper.Map<OrderItem>(form);

            // Assign shop to new promotion.
            var order = Context.Orders.SingleOrDefaultAsync(x => x.Id == form.OrderId, ct).Result;
            // Validation requested id.
            if (order == null)
            {
                // Create new order.
                order = new Order
                {
                    CustomerId = form.CreatedById,
                    PaymentState = OrderPaymentStateOptions.Cart,
                    State = OrderStateOptions.Cart
                };
                // Assign order to order item.
                orderItem.Order = order;
            }
            else
            {
                // Assign order to order item.
                orderItem.OrderId = order.Id;
            }

            // Calculate total price.
            orderItem.Total = orderItem.Quantity * orderItem.UnitPrice;

            // Calculate order fields.
            order.Total += orderItem.Total;
            order.ItemsTotal += orderItem.Quantity;
            order.UpdatedAt = DateTime.Now;

            return orderItem;
        }

        /// <inheritdoc />
        protected override void ValidateAddOrUpdateRequest(T form)
        {
        }
    }
}