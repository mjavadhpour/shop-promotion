// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;

namespace ShopPromotion.Domain.EntityLayer.LiteDb
{
    public class SmsUsage
    {
        public int Id { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsSucceed { get; set; }
    }
}