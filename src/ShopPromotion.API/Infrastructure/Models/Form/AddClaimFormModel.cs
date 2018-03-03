// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    /// <summary>
    /// Model for add claim to intended user.
    /// </summary>
    public class AddClaimFormModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Claim value.
        /// </summary>
        [Required]
        [RegularExpression(@"^AppUserClaim|ShopKeeperClaim*$")]
        public string Value { get; set; }
    }
}