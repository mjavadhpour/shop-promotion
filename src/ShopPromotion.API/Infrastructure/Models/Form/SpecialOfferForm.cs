// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    using Domain.EntityLayer;

    public class SpecialOfferForm : BaseEntity
    {
        [Required]
        public int ShopId { get; set; }

        public DateTime ExpireAt { get; set; }

        public bool IsEnabled { get; set; }

        public string Description { get; set; }
    }
}