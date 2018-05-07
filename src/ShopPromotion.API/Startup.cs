// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ShopPromotion.API
{
    using ServiceConfiguration;
    using Infrastructure.AppSettings;
    using Infrastructure.Data;
    using Domain.Infrastructure;
    using Domain.Infrastructure.AppSettings;
    using Helper.Infrastructure.Middleware;

    /// <summary>
    /// Startup project class.
    /// We must to register services here.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Cunstroctor for class Startup
        /// </summary>
        /// <param name="env">
        /// Provides information about the web hosting environment an application is running in.
        /// </param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Get system configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Database connection information.
            services.AddEntityFrameworkSqlServer().AddDbContext<ShopPromotionDomainContext>();
            // Add options to add required service for use services.Configure with no error.
            services.AddOptions();
            // Configure app settings
            services.Configure<ShopPromotionDomainAppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<ShopPromotionApiAppSettings>(Configuration.GetSection("AppSettings"));
            // DI for configuration
            services.AddSingleton<IConfiguration>(Configuration);
            // Add auto mapper.
            services.AddAutoMapper();
            // Lowercase routes
            services.AddRouting(options => options.LowercaseUrls = true);
            // Add Database Initializer
            services.AddTransient<IDbInitializer, DbInitializer>();
            // Swagger
            ConfigureSwaggerService.Configure(services);
            // User identity
            ConfigureIdentityService.Configure(services);
            // User authentication
            ConfigureJwtAuthService.Configure(services);
            // ShopPromotion API
            ConfigureShopPromotionApiService.Configure(services);
            // ShopPromotion core
            ConfigureShopPromotionDomainService.Configure(services);
            // Policy based role checks
            ConfigurePolicyService.Configure(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="dbInitializer"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
            IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // Use options from ShopPromotionMiddleware. allow OPTIONS method and more.
            app.UseShopPromotionOptionsMiddleware();
            // Use ShopPromotion headers middle-ware that allow us to registers all headers we want to request pipeline
            app.UseShopPromotionHeadersMiddleware(new ShopPromotionHeadersBuilder()
                .AddDefaultSecurePolicy()
                .AddCustomHeader("Access-Control-Allow-Origin", "*"));
            // Enable middle-ware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middle-ware to serve SWAGGER-UI (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopPromotion API V1"); });
            // Serialize errors as JSON
            var jsonExceptionMiddleware = new JsonExceptionMiddleware(
                app.ApplicationServices.GetRequiredService<IHostingEnvironment>());
            app.UseExceptionHandler(new ExceptionHandlerOptions {ExceptionHandler = jsonExceptionMiddleware.Invoke});
            // Generate EF Core Seed Data
            dbInitializer.Initialize().Wait();
            // NGinx Configure
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
