// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    /// <summary>
    /// Form model used in update base user API.
    /// </summary>
    public class UpdateUserFormModel
    {
        /// <summary>
        /// The user phone number.
        /// </summary>
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The user email.
        /// </summary>
        public string Email { get; set; }
    }
}