// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services.StatisticsServices
{
    using EntityLayer;
    using Infrastructure;
    using PaginationHelper;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Resource.Custom;

    /// <inheritdoc cref="IPaymentReportTracker" />
    public class PaymentReportService : BaseEntityService<ShopPromotionDomainContext>, IPaymentReportTracker
    {
        private IQueryable<Payment> _payments;

        public PaymentReportService(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context) : base(resolvedPaginationValue, context)
        {
            _payments = Context.Set<Payment>();
        }

        /// <inheritdoc />
        public async Task<NumberOfPaymentsReportViewModel> GetNumberOfPayments(
            IEntityTypeParameters reportParameters, CancellationToken ct)
        {
            _payments = FilterByRequestedParameters(reportParameters);

            var count = await _payments.CountAsync(ct);
            return new NumberOfPaymentsReportViewModel(count);
        }

        /// <inheritdoc />
        public async Task<SumOfPaymentsReportViewModel> GetSumOfPayments(IEntityTypeParameters reportParameters,
            CancellationToken ct)
        {
            _payments = FilterByRequestedParameters(reportParameters);

            var sum = await _payments.SumAsync(x => x.Amount, ct);
            return new SumOfPaymentsReportViewModel(sum);
        }

        /// <summary>
        /// Filter by intended parameters.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <returns></returns>
        private IQueryable<Payment> FilterByRequestedParameters(IEntityTypeParameters reportParameters)
        {
            // Filter by shop id.
            if (reportParameters.GetParameter("ShopId") != null)
                _payments = _payments.Where(x =>
                    x.Order.ShopPromotionBarcode.Promotion.ShopId == (int) reportParameters.GetParameter("ShopId"));

            // Filter by from price.
            if (reportParameters.GetParameter("FromPrice") != null)
                _payments = _payments.Where(x =>
                    x.Amount >= (int) reportParameters.GetParameter("FromPrice"));

            // Filter by to price.
            if (reportParameters.GetParameter("ToPrice") != null)
                _payments = _payments.Where(x =>
                    x.Amount <= (int) reportParameters.GetParameter("ToPrice"));

            // Filter by payment method id.
            if (reportParameters.GetParameter("PaymentMethodId") != null)
                _payments = _payments.Where(x =>
                    x.PaymentMothodId == (int) reportParameters.GetParameter("PaymentMethodId"));

            // Filter by from date.
            if (reportParameters.GetParameter("FromDate") != null)
                _payments = _payments.Where(x =>
                    x.CreatedAt >= (DateTime) reportParameters.GetParameter("FromDate"));

            // Filter by to date.
            if (reportParameters.GetParameter("ToDate") != null)
                _payments = _payments.Where(x =>
                    x.CreatedAt <= (DateTime) reportParameters.GetParameter("ToDate"));

            return _payments;
        }
    }
}