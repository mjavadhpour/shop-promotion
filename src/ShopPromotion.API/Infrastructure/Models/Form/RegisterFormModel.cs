// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    /// <summary>
    /// Model for registring ShopPromotion user.
    /// </summary>
    public class RegisterFormModel
    {
        /// <summary>
        /// The user phone number.
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }
    }
}