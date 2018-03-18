// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The discount coupons for order.
    /// </summary>
    public class OrderDiscountCoupon : BaseEntity
    {
        public int DiscountCouponId { get; set; }
        public DiscountCoupon DiscountCoupon { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}