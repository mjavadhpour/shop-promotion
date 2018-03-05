﻿// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.Domain.ModelLayer.Form
{
    using EntityLayer;

    public class MessageForm : BaseEntity
    {
        [Required]
        public string XXX { get; set; }
    }
}