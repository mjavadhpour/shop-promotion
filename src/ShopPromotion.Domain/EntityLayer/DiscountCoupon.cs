// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The discount coupon for order. This coupon was auto generated with given role from admin when owner checkout
    /// an order.
    /// </summary>
    public class DiscountCoupon : BaseEntity
    {
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }

        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }

        public string Code { get; set; }

        public int UsageLimit { get; set; }

        public int Used { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}