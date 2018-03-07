// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services.StatisticsServices
{
    // Domain
    using PaginationHelper;
    using Infrastructure;
    using Infrastructure.Models.Parameter.Custom;
    using Infrastructure.Models.Resource.Custom;

    /// <inheritdoc cref="IUserReportService" />
    public class UserReportService : BaseEntityService<ShopPromotionDomainContext>, IUserReportService
    {
        /// <inheritdoc />
        public UserReportService(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context) : base(resolvedPaginationValue, context)
        {
        }

        /// <inheritdoc />
        public async Task<AppUsersReportViewModel> GetNumberOfUsers(AppUsersReportParameters reportParameters,
            CancellationToken ct)
        {
            var appUserCount = await Context.AppUsers.CountAsync(ct);
            var shopKeeperUserCount = await Context.ShopKeeperUsers.CountAsync(ct);
            var adminUserCount = await Context.AdminUsers.CountAsync(ct);
            return new AppUsersReportViewModel(appUserCount, shopKeeperUserCount, adminUserCount);
        }
    }
}