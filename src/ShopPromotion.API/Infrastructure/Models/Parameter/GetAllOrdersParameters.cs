// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllOrdersParameters : IEntityTypeParameters
    {

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            return "null";
        }
    }
}