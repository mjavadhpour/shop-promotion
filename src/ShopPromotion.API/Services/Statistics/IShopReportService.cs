// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;

namespace ShopPromotion.API.Services.Statistics
{
    using Infrastructure.Models.ActionResults;
    using Infrastructure.Models.Parameter;

    /// <summary>
    /// Shop Report service.
    /// </summary>
    public interface IShopReportService
    {
        /// <summary>
        /// Get report for amount of shops with requested filters.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <returns></returns>
        Task<ShopsReportViewModel> GetNumberOfShops(ShopsReportParameters reportParameters);
    }
}