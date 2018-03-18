// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    public class MinimumDiscountResource : MinimumBaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UsageLimit { get; set; }

        public int Used { get; set; }

        public int MinimumOrderTotal { get; set; }

        public double DiscountPercent { get; set; }

        public bool Enabled { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}