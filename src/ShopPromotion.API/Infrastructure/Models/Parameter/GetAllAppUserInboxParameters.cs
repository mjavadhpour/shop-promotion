// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllAppUserInboxParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Filter by phone number.
        /// </summary>
        [FromRoute]
        [Required]
        public string PhoneNumber { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "PhoneNumber":
                    return PhoneNumber;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}