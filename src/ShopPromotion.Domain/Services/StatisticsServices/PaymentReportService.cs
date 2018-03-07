// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services.StatisticsServices
{
    using EntityLayer;
    using Infrastructure;
    using PaginationHelper;
    using Infrastructure.Models.Parameter.Custom;
    using Infrastructure.Models.Resource.Custom;

    /// <inheritdoc cref="IPaymentReportTracker" />
    public class PaymentReportService : BaseEntityService<ShopPromotionDomainContext>, IPaymentReportTracker
    {
        private readonly DbSet<Payment> _payments;

        public PaymentReportService(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context) : base(resolvedPaginationValue, context)
        {
            _payments = Context.Set<Payment>();
        }

        /// <inheritdoc />
        public async Task<NumberOfPaymentsReportViewModel> GetNumberOfPayments(
            PaymentsReportParameters reportParameters, CancellationToken ct)
        {
            var count = await _payments.CountAsync(ct);
            return new NumberOfPaymentsReportViewModel(count);
        }

        /// <inheritdoc />
        public async Task<SumOfPaymentsReportViewModel> GetSumOfPayments(PaymentsReportParameters reportParameters,
            CancellationToken ct)
        {
            var sum = await _payments.SumAsync(x => x.Amount, ct);
            return new SumOfPaymentsReportViewModel(sum);
        }
    }
}