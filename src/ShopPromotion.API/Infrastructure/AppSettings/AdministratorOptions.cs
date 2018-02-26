// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.AppSettings
{
    /// <summary>
    /// Token option class that keep token configurations.
    /// </summary>
    public class AdministratorOptions
    {
        /// <summary>
        /// Administrator user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Administrator email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Administrator password.
        /// </summary>
        public string Password { get; set; }
    }
}