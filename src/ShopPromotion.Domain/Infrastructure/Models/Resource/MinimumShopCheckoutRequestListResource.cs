// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    public class MinimumShopCheckoutRequestListResource : MinimumBaseEntity
    {
        [JsonIgnore]
        public override int Id { get; set; }

        public string CheckoutRequestId { get; set; }

        public IList<MinimumShopCheckoutRequestOrder> Orders { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}