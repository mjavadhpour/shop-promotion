// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    using Custom;
    using EntityLayer;

    public class MinimumMessageListResource : MinimumBaseEntity
    {
        public MinimumIdentityUserResource Author { get; set; }

        public MessageTargetTypeOption Target { get; set; }

        public string TargetObjectId { get; set; }

        public string Subject { get; set; }
    }
}