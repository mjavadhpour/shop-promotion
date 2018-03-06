// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Parameter
{
    public interface IEntityTypeParameters
    {
        /// <summary>
        /// This method return parameters with specific name.
        /// </summary>
        /// <param name="nameOfParam"></param>
        /// <returns></returns>
        object GetParameter(string nameOfParam);
    }
}