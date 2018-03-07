// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;

namespace ShopPromotion.Domain.Services.StatisticsServices
{
    using Infrastructure.Models.Parameter.Custom;
    using Infrastructure.Models.Resource.Custom;

    /// <summary>
    /// AppUser Report service.
    /// </summary>
    public interface IUserReportService
    {
        /// <summary>
        /// Get report for amount of users with requested filters.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<AppUsersReportViewModel> GetNumberOfUsers(AppUsersReportParameters reportParameters,
            CancellationToken ct);
    }
}