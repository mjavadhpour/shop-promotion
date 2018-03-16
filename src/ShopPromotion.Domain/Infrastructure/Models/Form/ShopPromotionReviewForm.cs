// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ShopPromotion.Domain.Infrastructure.Models.Form
{
    using EntityLayer;

    public class ShopPromotionReviewForm : BaseEntity
    { 
        [JsonIgnore]
        [Required]
        public virtual int ShopPromotionId { get; set; }

        public virtual string Comment { get; set; }

        public virtual int Rating { get; set; }
    }
}