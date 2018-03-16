// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ShopPromotion.Domain.Infrastructure.Models.Form
{
    using EntityLayer;

    public class ShopPromotionForm : BaseEntity
    {
        [JsonIgnore]
        [Required]
        public virtual int ShopId { get; set; }

        [FromBody]
        public virtual int? PaymentMethodId { get; set; }

        [FromBody]
        public virtual string Description { get; set; }

        [Required]
        [FromBody]
        public virtual string Name { get; set; }

        [Required]
        [FromBody]
        public virtual  int DiscountPercent { get; set; }

        [FromBody]
        public virtual DateTime StartAt { get; set; }

        [FromBody]
        public virtual DateTime EntAt { get; set; }

        [FromBody]
        public virtual int UsageLimit { get; set; }

        [FromBody]
        public virtual int MinimumPaymentAmount { get; set; }
    }
}