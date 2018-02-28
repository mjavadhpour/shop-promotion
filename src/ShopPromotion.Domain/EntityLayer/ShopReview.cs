// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// Shop Review is review of shop with AppUser. He/She can leave a comment or rate the shop.
    /// </summary>
    public class ShopReview : BaseEntity
    {
        public string AuthorId { get; set; }
        public AppUser Author { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }
    }
}