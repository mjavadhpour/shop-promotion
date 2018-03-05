// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;

namespace ShopPromotion.API.Services.Statistics
{
    using Infrastructure.Models.ActionResults;
    using Infrastructure.Models.Parameter;

    /// <summary>
    /// Usage report for showing in chart.
    /// </summary>
    public interface IUsageStatisticservice
    {
        /// <summary>
        /// Get usage report to showing in the chart.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <returns></returns>
        Task<UsagesStatisticsViewModel> GetUsagesChartReport(UsageStatisticsParameters reportParameters);
    }
}