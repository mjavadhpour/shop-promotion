// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllGeolocationsParameters : IEntityTypeParameters
    {
        [Required]
        public int ShopId { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "ShopId":
                    return ShopId;
                case "AttributeId":
                    return null;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}