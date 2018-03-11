// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    public class MinimumShopResource : MinimumBaseEntity
    {
        public string Title { get; set; }

        public string Image { get; set; }

        public IList<MinimumAttributeResource> Attributes { get; set; }
    }
}