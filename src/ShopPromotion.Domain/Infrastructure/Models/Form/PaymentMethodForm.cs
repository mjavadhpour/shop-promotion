// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.Domain.Infrastructure.Models.Form
{
    using EntityLayer;

    public class PaymentMethodForm : BaseEntity
    {
        public virtual int? GatewayConfigId { get; set; }

        [Required]
        public virtual string Code { get; set; }

        [Required]
        public virtual bool IsEnabled { get; set; }

        [Required]
        public virtual int Position { get; set; }
    }
}