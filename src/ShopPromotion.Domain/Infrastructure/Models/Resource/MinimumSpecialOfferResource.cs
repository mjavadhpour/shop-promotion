// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    public class MinimumSpecialOfferResource : MinimumBaseEntity
    {
        public MinimumShopListResource Shop { get; set; }

        public DateTime ExpireAt { get; set; }

        public bool IsEnabled { get; set; }

        public string Description { get; set; }
    }
}