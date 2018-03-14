﻿// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    using Domain.EntityLayer;

    public class MessageForm : BaseEntity
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public MessageTargetTypeOption TargetType { get; set; }

        public string TargetObjectId { get; set; }
    }
}