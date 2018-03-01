// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The Shop is a main object in this system. Customers get OFF with barcode that was registered for a Promotion
    /// of Shop.
    /// Shop has an owner, Admin and Owner can update the Shop.
    /// </summary>
    public class Shop : BaseEntity
    {
        public string OwnerId { get; set; }
        public ShopKeeperUser Owner { get; set; }
    }
}