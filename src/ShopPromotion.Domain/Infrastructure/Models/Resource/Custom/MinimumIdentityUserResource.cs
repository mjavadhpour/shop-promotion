// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource.Custom
{
    /// <summary>
    /// Identity user response model.
    /// </summary>
    public class MinimumIdentityUserResource
    {
        public string FullName { get; set; }

        public string ProfileImagePath { get; set; }

        public string PhoneNumber { get; set; }

        /// <summary>
        /// The user claim list.
        /// </summary>
        public IList<string> ClaimList { get; set; }
    }
}