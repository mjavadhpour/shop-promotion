// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using LiteDB;
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
    using Domain.Infrastructure;
    using Domain.Infrastructure.DAL;
    using Domain.Services.ShopStatusServices;
    using Domain.Services.StatisticsServices;

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
            //Unit of work
            services.AddScoped<UnitOfWork>();
            // ShopStatusService
            services.AddScoped<IShopPromotionUserManager, ShopPromotionUserManager>();
            // PaymentReportService
            services.AddScoped<IPaymentReportTracker, PaymentReportService>();
            // ShopReportService
            services.AddScoped<IShopReportService, ShopReportService>();
            // AppUserReportService
            services.AddScoped<IUserReportService, UserReportService>();
            // UsageStatisticservice
            services.AddScoped<IUsageStatisticservice, UsageStatisticservice>();
            // Generic unit of work. ShopAdmin
            services.AddScoped<UnitOfWork<ShopAdminForm, MinimumShopListResource, MinimumShopResource, Shop>>();
            // Generic unit of work. Shop
            services.AddScoped<UnitOfWork<ShopForm, MinimumShopListResource, MinimumShopResource, Shop>>();
            // Generic unit of work. Message
            services.AddScoped<UnitOfWork<MessageForm, MinimumMessageListResource, MinimumMessageResource, Message>>();
            // Generic unit of work. SpecialOffer
            services.AddScoped<UnitOfWork<SpecialOfferForm, MinimumSpecialOfferResource, MinimumSpecialOfferResource, SpecialOffer>>();
            // Generic unit of work. ShopPromotion
            services.AddScoped<UnitOfWork<ShopPromotionForm, MinimumShopPromotionResource, MinimumShopPromotionResource, ShopPromotion>>();
            // Generic unit of work. Order
            services.AddScoped<UnitOfWork<OrderForm, MinimumOrderResource, MinimumOrderResource, Order>>();
            // ShopStatusService
            services.AddScoped<IShopStatusService, ShopStatusService>();
            // Shop
            services
                .AddScoped<IBaseService<ShopForm, MinimumShopListResource, MinimumShopResource, Shop, ShopPromotionDomainContext>,
                    DefaultShopService>();
            // ShopPromotion
            services
                .AddScoped<IBaseService<ShopPromotionForm, MinimumShopPromotionResource, MinimumShopPromotionResource, ShopPromotion, ShopPromotionDomainContext>,
                    DefaultShopPromotionService>();
            // Order
            services
                .AddScoped<IBaseService<OrderForm, MinimumOrderResource, MinimumOrderResource, Order, ShopPromotionDomainContext>,
                    DefaultOrderService>();
            // Message
            services
                .AddScoped<IBaseService<MessageForm, MinimumMessageListResource, MinimumMessageResource, Message, ShopPromotionDomainContext>,
                    DefaultEntityService<MessageForm, MinimumMessageListResource, MinimumMessageResource, Message, ShopPromotionDomainContext>>();
            // SpecialOffer
            services
                .AddScoped<
                    IBaseService<SpecialOfferForm, MinimumSpecialOfferResource, MinimumSpecialOfferResource, SpecialOffer, ShopPromotionDomainContext
                    >, DefaultEntityService<SpecialOfferForm, MinimumSpecialOfferResource, MinimumSpecialOfferResource, SpecialOffer,
                        ShopPromotionDomainContext>>();
            // Shop Admin
            services
                .AddScoped<IBaseService<ShopAdminForm, MinimumShopListResource, MinimumShopResource, Shop, ShopPromotionDomainContext>,
                    DefaultAdminShopService>();
            // Report services.
            services.AddScoped<IUserReportService, UserReportService>();
            services.AddScoped<IPaymentReportTracker, PaymentReportService>();
            services.AddScoped<IShopReportService, ShopReportService>();
            services.AddScoped<IUsageStatisticservice, UsageStatisticservice>();
            // Cunstomized user manager.
            services.AddTransient<IShopPromotionUserManager, ShopPromotionUserManager>();
            // LightDb
            // TODO: Read path from appsettings.json
            services.AddSingleton(typeof(LiteDatabase), new LiteDatabase(@"UsageTracker.db"));
        }
    }
}