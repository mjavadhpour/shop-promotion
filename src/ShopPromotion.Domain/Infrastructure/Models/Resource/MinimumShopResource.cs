// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using ShopPromotion.Domain.EntityLayer;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    public class MinimumShopResource : MinimumBaseEntity
    {
        public string Title { get; set; }

        public IList<MinimumShopImageResource> Images { get; set; }

        public IList<MinimumAttributeResource> Attributes { get; set; }

        public MinimumShopGeolocationResource Geolocation { get; set; }

        public ShopStatusOption Status { get; set; }

        public string Instagram { get; set; }

        public string Telegram { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }
    }
}