// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ShopPromotion.API.ServiceConfiguration
{
    // API
    using Services; 
    using EventHandlers;
    // Doamin
    using Domain.Infrastructure.Models.Resource;

    /// <summary>
    /// Configure ShopPromotion api services.
    /// </summary>
    public class ConfigureShopPromotionApiService : IConfigureService
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
            // Token provider.
            services.AddTransient<TokenProviderService>();
            // SMS APIs.
            services.AddTransient<SmsIrRestful.Token>();
            services.AddTransient<SmsIrRestful.MessageSend>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            // MediatR
            services.AddTransient<SingleInstanceFactory>(sp => t => sp.GetService(t));
            services.AddTransient<MultiInstanceFactory>(sp => t => sp.GetServices(t));
            services.AddMediatR(typeof(Startup), typeof(EntityCreatedEventHandler));
        }
    }
}