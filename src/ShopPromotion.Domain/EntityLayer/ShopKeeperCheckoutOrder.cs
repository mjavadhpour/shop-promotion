// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// Keep reference of orders to checkout for shop keeper when admin approved the checkout request.
    /// </summary>
    public class ShopKeeperCheckoutOrder : BaseEntity
    {
        public int ShopKeeperCheckoutId { get; set; }
        public ShopKeeperCheckout ShopKeeperCheckout { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}