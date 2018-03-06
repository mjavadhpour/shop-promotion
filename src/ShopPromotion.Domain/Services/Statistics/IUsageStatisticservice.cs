// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;

namespace ShopPromotion.Domain.Services.Statistics
{
    using Infrastructure.Models.Parameter.Custom;
    using Infrastructure.Models.Resource.Custom;

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