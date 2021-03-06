// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Response
{
    public class SingleModelResponse<TModel> : ISingleModelResponse<TModel>
    {
        public TModel Model { get; set; }
    }
}