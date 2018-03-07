// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services.Statistics
{
    using EntityLayer.LiteDb;
    using Infrastructure;
    using Infrastructure.Models.Parameter.Custom;
    using Infrastructure.Models.Resource.Custom;
    using PaginationHelper;

    /// <inheritdoc cref="IUsageStatisticservice" />
    public class UsageStatisticservice : BaseEntityService<ShopPromotionDomainContext>, IUsageStatisticservice
    {
        private LiteDatabase _liteDatabase;

        public UsageStatisticservice(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context, LiteDatabase liteDatabase) : base(resolvedPaginationValue, context)
        {
            _liteDatabase = liteDatabase;
        }

        /// <inheritdoc />
        public async Task<IList<UsagesStatisticsViewModel>> GetUsagesChartReport(
            UsageStatisticsParameters reportParameters, CancellationToken ct)
        {
            var usageStatisticsList = new List<UsagesStatisticsViewModel>();

            for (var date = reportParameters.FromDate.Date; date <= reportParameters.ToDate; date = date.AddDays(1))
            {
                var currentDate = date;
                var users = await Context.BaseIdentityUsers.Where(
                        x => x.CreatedAt  >= currentDate.Date && x.CreatedAt < currentDate.Date.AddDays(1))
                    .CountAsync(ct);
                var shops = await Context.Shops.Where(
                        x => x.CreatedAt  >= currentDate.Date && x.CreatedAt < currentDate.Date.AddDays(1))
                    .CountAsync(ct);
                var payments = await Context.Payments.Where(
                        x => x.CreatedAt  >= currentDate.Date && x.CreatedAt < currentDate.Date.AddDays(1))
                    .CountAsync(ct);
                var sms = _liteDatabase.GetCollection<SmsUsage>()
                    .Find(
                        x => x.SentAt >= currentDate.Date && x.SentAt < currentDate.Date.AddDays(1))
                    .Count();
                usageStatisticsList.Add(new UsagesStatisticsViewModel(date, users, shops, payments, sms, 0));
            }

            return usageStatisticsList;
        }
    }
}