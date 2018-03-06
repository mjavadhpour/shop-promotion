// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Parameter
{
    /// <summary>
    ///     Use for APIs that have no filter parameters.
    /// </summary>
    /// <remarks>Null</remarks>
    public class GetNullParameters : IEntityTypeParameters
    {
       public object GetParameter(string param)
        {
            return null;
        }
    }
}
