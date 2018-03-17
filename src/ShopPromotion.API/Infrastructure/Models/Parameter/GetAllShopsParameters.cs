// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllShopsParameters : FilterByCreateDate, IEntityTypeParameters
    {
        /// <summary>
        /// Filter by requested attribute id.
        /// </summary>
        public int? AttributeId { get; set; }

        public GeolocationPoint GeolocationPoint { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "CreateDate":
                    return CreateDate;
                case "AttributeId":
                    return AttributeId;
                case "GeolocationPoint":
                    return GeolocationPoint;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}