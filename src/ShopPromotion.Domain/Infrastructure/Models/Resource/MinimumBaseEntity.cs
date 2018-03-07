// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using Newtonsoft.Json;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    public class MinimumBaseEntity
    {
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
    }
}