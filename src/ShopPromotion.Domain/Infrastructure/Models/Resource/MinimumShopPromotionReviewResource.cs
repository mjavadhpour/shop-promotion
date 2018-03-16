// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using ShopPromotion.Domain.Infrastructure.Models.Resource.Custom;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    public class MinimumShopPromotionReviewResource : MinimumBaseEntity
    {
        public MinimumIdentityUserResource Author { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }

        public MinimumShopPromotionResource ShopPromotion { get; set; }
    }
}