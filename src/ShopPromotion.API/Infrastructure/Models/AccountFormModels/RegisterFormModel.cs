// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.Models.AccountFormModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Model for registring ShopPromotion user.
    /// </summary>
    public class RegisterFormModel
    {
        /// <summary>
        /// The user phone number.
        /// </summary>
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}