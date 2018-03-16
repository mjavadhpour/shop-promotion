// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    using Custom;

    public class MinimumShopPromotionLikeListResource : MinimumBaseEntity
    {
        public MinimumIdentityUserResource LikedBy { get; set; }
    }
}