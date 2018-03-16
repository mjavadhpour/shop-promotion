// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// Promotion Like.
    /// </summary>
    public class ShopPromotionLike : BaseEntity
    {
        public ShopPromotionLike()
        {
            Liked = false;
        }

        public string LikedById { get; set; }
        public BaseIdentityUser LikedBy { get; set; }

        public int ShopPromotionId { get; set; }
        public ShopPromotion ShopPromotion { get; set; }

        public bool Liked { get; set; }
    }
}