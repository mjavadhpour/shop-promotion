// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ShopPromotion.API.Infrastructure.ActionResults
{
    using Models.Resource;

    /// <summary>
    /// View model for token response.
    /// </summary>
    public class TokenViewModel
    {
        /// <summary>
        /// Token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Velid to.
        /// </summary>
        public DateTime ExpiresIn { get; set; }

        /// <summary>
        /// Bearer token type by default.
        /// </summary>
        public string TokenType => JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// User claim.
        /// </summary>
        public IList<MinimumClaimResource> Claim { get; set; }
    }
}