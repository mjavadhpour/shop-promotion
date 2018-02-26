// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;

namespace ShopPromotion.API.Infrastructure.ActionResults
{
    using Models;

    /// <summary>
    /// View model for token response.
    /// </summary>
    public class TokenViewModel
    {
        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Velid to.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// User claim.
        /// </summary>
        public IList<MinimumClaimResource> Claim { get; set; }
    }
}