// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllShopCheckoutRequestsParameters : FilterByCreateDate, IEntityTypeParameters
    {
        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
             return null;
        }
    }
}