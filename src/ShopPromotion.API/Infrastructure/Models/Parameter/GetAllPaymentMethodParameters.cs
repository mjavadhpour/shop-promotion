// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllPaymentMethodParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Gateway Config id.
        /// </summary>
        public int? GatewayConfigId { get; set; }

        /// <summary>
        /// The current logged in user.
        /// </summary>
        [JsonIgnore]
        public string CurrentUserId { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "GatewayConfigId":
                    return GatewayConfigId;
                case "CurrentUserId":
                    return CurrentUserId;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}