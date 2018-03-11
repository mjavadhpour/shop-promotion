// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The promotion for a Shop. Promotion known as advertisement in this system. 
    /// </summary>
    public class ShopPromotion : BaseEntity
    {
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public int? PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
        
        public int DiscountPercent { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EntAt { get; set; }

        public int UsageLimit { get; set; }

        public int Used { get; set; }

        public int MinimumPaymentAmount { get; set; }

        public double AverageRating { get; set; }
    }
}