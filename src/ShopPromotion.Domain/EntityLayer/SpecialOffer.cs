// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The SpecialOffer is a Shop that was defined by AdminUser and will offer to AppUsers as a Special Offer. 
    /// </summary>
    public class SpecialOffer : BaseEntity
    {
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}