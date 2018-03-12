// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    using EntityLayer;

    public class MinimumMessageTargetResource : MinimumBaseEntity
    {
        public MessageTargetTypeOption TargetType { get; set; }

        public string TargetObjectId { get; set; }
    }
}