// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The message for sent to Shop or ShopKeeper or AppUser or All of them.
    /// </summary>
    public class Message : BaseEntity
    {
        public string AuthorId { get; set; }
        public AdminUser Author { get; set; }

        public string Note { get; set; }
    }
}