// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopPromotion.Domain.Infrastructure;
using ShopPromotion.Domain.Services.PaginationHelper;

namespace ShopPromotion.Domain.Services.Statistics
{
    using Infrastructure.Models.Parameter.Custom;
    using Infrastructure.Models.Resource.Custom;
    using EntityLayer;

    /// <inheritdoc cref="IShopReportService" />
    public class ShopReportService : BaseEntityService<ShopPromotionDomainContext>, IShopReportService
    {
        private readonly DbSet<Shop> _shops;

        public ShopReportService(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context) : base(resolvedPaginationValue, context)
        {
            _shops = Context.Set<Shop>();
        }

        /// <inheritdoc />
        public async Task<ShopsReportViewModel> GetNumberOfShops(ShopsReportParameters reportParameters, CancellationToken ct)
        {
            var count = await _shops.CountAsync(ct);
            return new ShopsReportViewModel(count);
        }
    }
}