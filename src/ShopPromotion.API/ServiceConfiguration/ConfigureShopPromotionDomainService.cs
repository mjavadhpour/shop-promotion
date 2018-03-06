// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.Extensions.DependencyInjection;

namespace ShopPromotion.API.ServiceConfiguration
{
    // Helper
    using Helper.Infrastructure.Filters;
    // Domain
    using Domain.EntityLayer;
    using Domain.Services;
    using Domain.Services.PaginationHelper;
    using Domain.Infrastructure.Models.Form;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Services.Statistics;

    /// <summary>
    /// Configure ShopPromotion core services.
    /// </summary>
    public class ConfigureShopPromotionDomainService : IConfigureService
    {
        /// <inheritdoc />
        void IConfigureService.Configure(IServiceCollection services)
        {
            Configure(services);
        }

        /// <summary>
        /// Real method for configuration.
        /// </summary>
        /// <remarks>
        /// Services that consume EF Core objects (DbContext) should be registered as Scoped
        /// see: <a href="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection#registering-your-own-services"> registring your own service</a>
        /// </remarks>
        /// <param name="services"></param>
        public static void Configure(IServiceCollection services)
        {
            // Helper class for use in pagination required values.
            services.AddScoped<ResolvedPaginationValueService>();
            // Filter with resolved DI
            services.AddScoped<PaginationDefaultValueFilter>();
            // Shop
            services.AddScoped<IBaseService<ShopForm, MinimumShopResource, Shop>, DefaultShopService>();
            // Report services.
            services.AddScoped<IAppUserReportService, AppUserReportService>();
            services.AddScoped<IPaymentReportTracker, PaymentReportService>();
            services.AddScoped<IShopReportService, ShopReportService>();
            services.AddScoped<IUsageStatisticservice, UsageStatisticservice>();
            // Cunstomized user manager.
            services.AddTransient<IShopPromotionUserManager, ShopPromotionUserManager>();
        }
    }
}