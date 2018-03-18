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
    using Extensions;
    using Infrastructure;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using PaginationHelper;

    public class
        DefaultOrderDiscountCouponService<T> : DefaultEntityService<T, MinimumOrderDiscountCouponListResource,
            MinimumOrderDiscountCouponResource, OrderDiscountCoupon,
            ShopPromotionDomainContext>
        where T : OrderDiscountCouponForm
    {
        public DefaultOrderDiscountCouponService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<OrderDiscountCoupon> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<OrderDiscountCoupon> GetElementsOfTModelSequenceAsync(
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
        protected override OrderDiscountCoupon MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map orderDiscountCoupon and resource.
            var orderDiscountCoupon = Mapper.Map<OrderDiscountCoupon>(form);

            // Find discount coupon from given code.
            var discountCoupon = Context.DiscountCoupons
                .SingleOrDefaultAsync(x => x.Code == form.DiscountCouponCode && x.OwnerId == form.CreatedById, ct)
                .Result;
            if (discountCoupon == null) throw new DiscountCouponNotFoundException();
            // Assign finded discount coupon to order discount coupon.
            orderDiscountCoupon.DiscountCouponId = discountCoupon.Id;

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
                orderDiscountCoupon.Order = order;
            }
            else
            {
                // Assign order to order item.
                orderDiscountCoupon.OrderId = order.Id;
            }

            return orderDiscountCoupon;
        }

        /// <inheritdoc />
        protected override void ValidateAddOrUpdateRequest(T form)
        {
        }
    }
}