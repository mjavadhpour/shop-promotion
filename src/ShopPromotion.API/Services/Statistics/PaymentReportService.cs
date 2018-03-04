﻿// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;

namespace ShopPromotion.API.Services.Statistics
{
    using Infrastructure.Models.Parameter;

    /// <inheritdoc />
    public class PaymentReportService : IPaymentReportTracker
    {
        /// <inheritdoc />
        public Task<object> GetNumberOfPayments(PaymentsReportParameters reportParameters)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<object> GetSumOfPayments(PaymentsReportParameters reportParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}