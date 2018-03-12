// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.Domain.Infrastructure.Models.Parameter
{
    public class GetAllSpecialOffersParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Filter special offers by IsEnabled.
        /// </summary>
        [FromQuery]
        public bool? IsEnabled { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "IsEnabled":
                    return IsEnabled;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}