// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;

namespace ShopPromotion.Domain.Services.Statistics
{
    using Infrastructure.Models.Parameter.Custom;
    using Infrastructure.Models.Resource.Custom;

    /// <inheritdoc />
    public class UsageStatisticservice : IUsageStatisticservice
    {
        /// <inheritdoc />
        public Task<UsagesStatisticsViewModel> GetUsagesChartReport(UsageStatisticsParameters reportParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}