// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The Geolocation of a Shop. 
    /// </summary>
    public class ShopGeolocation : BaseEntity
    {
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}