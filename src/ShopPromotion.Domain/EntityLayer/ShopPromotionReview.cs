// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// Promotion Review is review of promotion by AppUsers or ShopKeepers. He/She can leave a comment or rate the promotion of shop.
    /// </summary>
    public class ShopPromotionReview : BaseEntity
    {
        public string AuthorId { get; set; }
        public AppUser Author { get; set; }

        public int ShopPromotionId { get; set; }
        public ShopPromotion ShopPromotion { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }
    }
}