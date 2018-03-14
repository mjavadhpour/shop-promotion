// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopPromotion.Domain.Infrastructure;
using ShopPromotion.Domain.Services.PaginationHelper;

namespace ShopPromotion.Domain.Services.StatisticsServices
{
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Resource.Custom;
    using EntityLayer;

    /// <inheritdoc cref="IShopReportService" />
    public class ShopReportService : BaseEntityService<ShopPromotionDomainContext>, IShopReportService
    {
        private IQueryable<Shop> _shops;

        public ShopReportService(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context) : base(resolvedPaginationValue, context)
        {
            _shops = Context.Set<Shop>();
        }

        /// <inheritdoc />
        public async Task<ShopsReportViewModel> GetNumberOfShops(IEntityTypeParameters reportParameters, CancellationToken ct)
        {
            // Filter by from date.
            if (reportParameters.GetParameter("FromDate") != null)
                _shops = _shops.Where(x =>
                    x.CreatedAt >= (DateTime) reportParameters.GetParameter("FromDate"));

            // Filter by to date.
            if (reportParameters.GetParameter("ToDate") != null)
                _shops = _shops.Where(x =>
                    x.CreatedAt <= (DateTime) reportParameters.GetParameter("ToDate"));

            // Filter by to date.
            if (reportParameters.GetParameter("Title") != null)
                _shops = _shops.Where(x =>
                    x.Title.Contains((string) reportParameters.GetParameter("Title")));

            var count = await _shops.CountAsync(ct);
            return new ShopsReportViewModel(count);
        }
    }
}