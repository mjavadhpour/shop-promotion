// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The order, or the shopping cart. App user can applied Promotion or Coupon to discount the final price for payment.
    /// </summary>
    public class Order : BaseEntity
    {
        public Order()
        {
            Code = Guid.NewGuid();
        }

        public string CustomerId { get; set; }
        public AppUser Customer { get; set; }

        public IList<OrderDiscountCoupon> DiscountCoupons { get; set; }

        public IList<OrderItem> OrderItems { get; set; }

        public int ShopPromotionBarcodeId { get; set; }
        public ShopPromotionBarcode ShopPromotionBarcode { get; set; }

        public int Total { get; set; }

        public int ItemsTotal { get; set; }

        public OrderStateOptions State { get; set; }
 
        public OrderPaymentStateOptions PaymentState { get; set; }

        public string Notes { get; set; }

        public Guid Code { get; set; }

        public DateTime CheckoutCompletedAt { get; set; }
    }
}