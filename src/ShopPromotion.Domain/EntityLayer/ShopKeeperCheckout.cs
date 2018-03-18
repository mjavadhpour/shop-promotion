// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// A base table for shop keeper checkout request given to admin.
    /// </summary>
    public class ShopKeeperCheckout : BaseEntity
    {
        public string ShopKeeperUserId { get; set; }
        public ShopKeeperUser ShopKeeperUser { get; set; }

        public IList<ShopKeeperCheckoutOrder> Orders { get; set; }
    }
}