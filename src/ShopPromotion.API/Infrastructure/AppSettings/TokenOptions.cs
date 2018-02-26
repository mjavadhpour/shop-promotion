// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.AppSettings
{
    /// <summary>
    /// Token option class that keep token configurations.
    /// </summary>
    public class TokenOptions
    {
        /// <summary>
        /// Issuer for token.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Key for token.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Audience for token.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Expires time of token in minutes.
        /// </summary>
        public double ExpiresTimeAsMinutes { get; set; }
    }
}