// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    using Custom;
    using EntityLayer;

    public class MinimumMessageResource : MinimumBaseEntity
    {
        public MinimumIdentityUserResource Author { get; set; }

        public IList<MinimumMessageTargetResource> MessageTargets { get; set; }

        public string Note { get; set; }

        public string Subject { get; set; }
    }
}