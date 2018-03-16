// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllShopOrdersParameters : FilterByCreateDate, IEntityTypeParameters
    {
        /// <summary>
        /// This field will set with logged in user's shop.
        /// </summary>
        [JsonIgnore]
        public int? ShopId { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "ShopId":
                    return ShopId;
                case "CreateDate":
                    return CreateDate;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}