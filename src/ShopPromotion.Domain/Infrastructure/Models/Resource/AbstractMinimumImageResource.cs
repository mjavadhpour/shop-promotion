// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Resource
{
    /// <summary>
    /// Minimum resource for image entity.
    /// TODO: validate with specific interface.
    /// </summary>
    public abstract class AbstractMinimumImageResource : MinimumBaseEntity
    {
        public string Path { get; set; }

        public string Type { get; set; }
    }
}