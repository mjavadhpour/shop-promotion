// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.API.Infrastructure.Models.Resource
{
    /// <summary>
    /// Identity user response model.
    /// </summary>
    public class MinimumIdentityUserResource
    {
        /// <summary>
        /// The User name.
        /// </summary>
        public object UserName;

        /// <summary>
        /// The user claim list.
        /// </summary>
        public IList<string> ClaimList { get; set; }
    }
}