﻿// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;

namespace ShopPromotion.Domain.Services.Statistics
{
    using Infrastructure.Models.Parameter.Custom;
    using Infrastructure.Models.Resource.Custom;

    /// <summary>
    /// Payment Report service.
    /// </summary>
    public interface IPaymentReportTracker
    {
        /// <summary>
        /// Get report for number of payments with requested filters.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<NumberOfPaymentsReportViewModel> GetNumberOfPayments(PaymentsReportParameters reportParameters,
            CancellationToken ct);

        /// <summary>
        /// Get report for sum of payments amount with requested filters.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<SumOfPaymentsReportViewModel> GetSumOfPayments(PaymentsReportParameters reportParameters,
            CancellationToken ct);
    }
}