// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.Domain.Infrastructure.Models.Parameter
{
    using EntityLayer;

    public class GetAllMessagesParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Filter messsages by target
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