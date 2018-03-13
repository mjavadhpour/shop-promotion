// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    using Domain.EntityLayer;

    public class ShopPromotionBarcodeForm : BaseEntity
    {
        [Required]
        public int PromotionId { get; set; }

        public string Barcode { get; set; }
    }
}