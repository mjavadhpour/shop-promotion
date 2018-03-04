﻿// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;
using ShopPromotion.API.Infrastructure.Models.Parameter;

namespace ShopPromotion.API.Services.Statistics
{
    /// <inheritdoc />
    public class ShopReportService : IShopReportService
    {
        /// <inheritdoc />
        public Task<object> GetNumberOfShops(ShopsReportParameters reportParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}