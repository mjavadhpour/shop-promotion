// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading.Tasks;
using ShopPromotion.Domain.Services.InboxServices;

namespace ShopPromotion.Domain.Infrastructure.DAL
{
    using Services.UserManager;
    using Services.ShopStatusServices;
    using Services.StatisticsServices;

    /// <summary>
    /// The unit of work class serves one purpose: to make sure that when you use multiple repositories, they share a
    /// single database context.
    /// </summary>
    /// <remarks>
    /// Used for custom entity services.
    /// </remarks>
    public class UnitOfWork : IDisposable
    {
        private readonly ShopPromotionDomainContext _context;
        private readonly IShopPromotionUserManager _shopPromotionUserManager;
        private readonly IShopStatusService _shopStatusService;
        private readonly IUsageStatisticservice _usageStatisticservice;
        private readonly IUserReportService _userReportService;
        private readonly IShopReportService _shopReportService;
        private readonly IPaymentReportTracker _paymentReportService;
        private readonly IInboxService _inboxService;

        public UnitOfWork(ShopPromotionDomainContext context, IShopPromotionUserManager shopPromotionUserManager,
            IShopStatusService shopStatusService, IUsageStatisticservice usageStatisticservice,
            IUserReportService userReportService, IShopReportService shopReportService,
            IPaymentReportTracker paymentReportService,
            IInboxService inboxService)
        {
            _context = context;
            _shopPromotionUserManager = shopPromotionUserManager;
            _shopStatusService = shopStatusService;
            _usageStatisticservice = usageStatisticservice;
            _userReportService = userReportService;
            _shopReportService = shopReportService;
            _paymentReportService = paymentReportService;
            _inboxService = inboxService;
        }

        public IShopPromotionUserManager ShopPromotionUserManager => _shopPromotionUserManager;

        public IShopStatusService ShopStatusService => _shopStatusService;

        public IUsageStatisticservice UsageStatisticservice => _usageStatisticservice;

        public IUserReportService UserReportService => _userReportService;

        public IShopReportService ShopReportService => _shopReportService;

        public IPaymentReportTracker PaymentReportService => _paymentReportService;

        public IInboxService InboxService => _inboxService;

        public ShopPromotionDomainContext Context => _context;

        /// <summary>
        /// Commit with the resolved context.
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}