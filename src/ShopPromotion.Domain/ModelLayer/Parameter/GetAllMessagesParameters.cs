// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.Domain.ModelLayer.Parameter
{
    public class GetAllMessagesParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Filter human by contract id
        /// </summary>
        [FromQuery]
        public int? XXX { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "XXX":
                    return XXX;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}