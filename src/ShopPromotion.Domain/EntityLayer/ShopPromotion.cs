// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The promotion for a Shop. Promotion known as advertisement in this system. 
    /// </summary>
    public class ShopPromotion : BaseEntity
    {
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}