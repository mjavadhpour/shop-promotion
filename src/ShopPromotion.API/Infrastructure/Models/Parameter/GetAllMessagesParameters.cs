// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.EntityLayer;
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllMessagesParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Filter by Target type. Requirenment: <br />
        /// Shop : 0 <br />
        /// AppUser: 1 <br />
        /// ShopKeeper: 2 <br />
        /// All: 3
        /// </summary>
        [FromQuery]
        public MessageTargetTypeOption Target { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "Target":
                    return Target;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}