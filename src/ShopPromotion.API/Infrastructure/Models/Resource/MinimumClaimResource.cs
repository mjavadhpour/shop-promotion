// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.Models.Resource
{
    /// <summary>
    /// Minimum claim resource.
    /// </summary>
    public class MinimumClaimResource
    {
        /// <summary>
        /// Claim type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Claim value.
        /// </summary>
        public string Value { get; set; }
    }
}