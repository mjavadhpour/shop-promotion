// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.Domain.ModelLayer.Parameter
{
    public class GetAllShopsParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Filter human by contract id
        /// </summary>
        [FromQuery]
        public int? ContractId { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "ContractId":
                    return ContractId;
                default:
                    return ContractId;
            }
        }
    }
}