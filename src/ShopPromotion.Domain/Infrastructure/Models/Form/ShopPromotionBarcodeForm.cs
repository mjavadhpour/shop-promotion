// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ShopPromotion.Domain.Infrastructure.Models.Form
{
    using EntityLayer;

    public class ShopPromotionBarcodeForm : BaseEntity
    {
        [Required]
        [JsonIgnore]
        public virtual int PromotionId { get; set; }

        public virtual string Barcode { get; set; }
    }
}