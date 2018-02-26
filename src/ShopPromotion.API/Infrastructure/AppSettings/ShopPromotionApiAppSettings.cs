// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.AppSettings
{
    /// <summary>
    /// Token option class that keep token configurations.
    /// </summary>
    public class ShopPromotionApiAppSettings
    {
        /// <summary>
        /// Token options.
        /// </summary>
        public TokenOptions TokenOptions { get; set; }

        /// <summary>
        /// Administrator options.
        /// </summary>
        public AdministratorOptions AdministratorOptions { get; set; }
    }
}