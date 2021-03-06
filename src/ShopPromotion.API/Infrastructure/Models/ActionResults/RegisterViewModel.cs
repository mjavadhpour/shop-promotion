// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.API.Infrastructure.Models.ActionResults
{
    using Models.Resource;

    /// <summary>
    /// View model for token response.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// The phone number of new user
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Is requested user new?
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// The default claim of new user
        /// </summary>
        public IList<MinimumClaimResource> Claim { get; set; }
    }
}