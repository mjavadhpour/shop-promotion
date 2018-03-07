// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;

namespace ShopPromotion.Domain.EntityLayer
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The ASP.NET Base identity user class. The drived class from this class was keeped in one table but with
    /// dofference discriminator.
    /// </summary>
    public abstract class BaseIdentityUser : IdentityUser
    {
        protected BaseIdentityUser()
        {
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Verification code for SMS login confirmation.
        /// </summary>
        public string VerificationCode { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}