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
    // Domain
    using EntityLayer;
    using PaginationHelper;
    using Infrastructure;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Resource.Custom;

    /// <inheritdoc cref="IUserReportService" />
    public class UserReportService : BaseEntityService<ShopPromotionDomainContext>, IUserReportService
    {
        private IQueryable<AppUser> _appUsers;
        private IQueryable<AdminUser> _adminUsers;
        private IQueryable<ShopKeeperUser> _shopKeeperUsers;

        /// <inheritdoc />
        public UserReportService(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context) : base(resolvedPaginationValue, context)
        {
            _adminUsers = Context.Set<AdminUser>();
            _appUsers = Context.Set<AppUser>();
            _shopKeeperUsers = Context.Set<ShopKeeperUser>();
        }

        /// <inheritdoc />
        public async Task<AppUsersReportViewModel> GetNumberOfUsers(IEntityTypeParameters reportParameters,
            CancellationToken ct)
        {
            var appUserCount = await FilterAppUsersByRequestedParameters(reportParameters).CountAsync(ct);
            var shopKeeperUserCount = await FilterShopKeeperUsersByRequestedParameters(reportParameters).CountAsync(ct);
            var adminUserCount = await FilterAdminUsersByRequestedParameters(reportParameters).CountAsync(ct);
            return new AppUsersReportViewModel(appUserCount, shopKeeperUserCount, adminUserCount);
        }

        /// <summary>
        /// Filter by intended parameters.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <returns></returns>
        private IQueryable<AppUser> FilterAppUsersByRequestedParameters(IEntityTypeParameters reportParameters)
        {
            // Filter by from date.
            if (reportParameters.GetParameter("FromDate") != null)
                _appUsers = _appUsers.Where(x =>
                    x.CreatedAt >= (DateTime) reportParameters.GetParameter("FromDate"));

            // Filter by to date.
            if (reportParameters.GetParameter("ToDate") != null)
                _appUsers = _appUsers.Where(x =>
                    x.CreatedAt <= (DateTime) reportParameters.GetParameter("ToDate"));

            return _appUsers;
        }

        /// <summary>
        /// Filter by intended parameters.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <returns></returns>
        private IQueryable<AdminUser> FilterAdminUsersByRequestedParameters(IEntityTypeParameters reportParameters)
        {
            // Filter by from date.
            if (reportParameters.GetParameter("FromDate") != null)
                _adminUsers = _adminUsers.Where(x =>
                    x.CreatedAt >= (DateTime) reportParameters.GetParameter("FromDate"));

            // Filter by to date.
            if (reportParameters.GetParameter("ToDate") != null)
                _adminUsers = _adminUsers.Where(x =>
                    x.CreatedAt <= (DateTime) reportParameters.GetParameter("ToDate"));

            return _adminUsers;
        }

        /// <summary>
        /// Filter by intended parameters.
        /// </summary>
        /// <param name="reportParameters"></param>
        /// <returns></returns>
        private IQueryable<ShopKeeperUser> FilterShopKeeperUsersByRequestedParameters(
            IEntityTypeParameters reportParameters)
        {
            // Filter by from date.
            if (reportParameters.GetParameter("FromDate") != null)
                _shopKeeperUsers = _shopKeeperUsers.Where(x =>
                    x.CreatedAt >= (DateTime) reportParameters.GetParameter("FromDate"));

            // Filter by to date.
            if (reportParameters.GetParameter("ToDate") != null)
                _shopKeeperUsers = _shopKeeperUsers.Where(x =>
                    x.CreatedAt <= (DateTime) reportParameters.GetParameter("ToDate"));

            return _shopKeeperUsers;
        }
    }
}