// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The address, phone number, and other address related information of shop.
    /// </summary>
    public class ShopAddress : BaseEntity
    {
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}