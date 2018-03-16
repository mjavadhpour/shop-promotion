// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using ShopPromotion.Domain.Infrastructure.Models.Resource.Custom;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    public class MinimumShopPromotionLikeResource : MinimumBaseEntity
    {
        public MinimumIdentityUserResource LikedBy { get; set; }

        public MinimumShopPromotionResource ShopPromotion { get; set; }
    }
}