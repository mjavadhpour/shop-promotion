// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    using Domain.EntityLayer;

    public class ShopForm : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Instagram { get; set; }

        public string Telegram { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }
    }
}