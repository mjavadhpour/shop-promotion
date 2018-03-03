// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// Generated barcode for promotion to approve an discount for an Order.
    /// </summary>
    public class ShopPromotionBarcode : BaseEntity
    {        
        public int PromotionId { get; set; }
        public ShopPromotion Promotion { get; set; }
    }
}