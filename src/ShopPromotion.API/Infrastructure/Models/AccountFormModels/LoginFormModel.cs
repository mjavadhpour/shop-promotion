// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.AccountFormModels
{
    /// <summary>
    /// Login model.
    /// </summary>
    public class LoginFormModel
    {
        /// <summary>
        /// The verification code that was send to mobile phone with SMS.
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}