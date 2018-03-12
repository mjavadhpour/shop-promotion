// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace ShopPromotion.API.ServiceConfiguration
{
    /// <summary>
    /// Configure swagger service.
    /// </summary>
    public class ConfigureSwaggerService : IConfigureService
    {
        /// <inheritdoc />
        void IConfigureService.Configure(IServiceCollection services)
        {
            Configure(services);
        }

        /// <summary>
        /// Real method for configuration.
        /// </summary>
        /// <param name="services"></param>
        public static void Configure(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ShopPromotion API",
                    Version = "v1",
                    Description = "ShopPromotion Startup ASP.NET Core Web API",
                    Contact = new Contact
                    {
                        Name = "Mohammad Javad HoseinPour",
                        Email = "mjavadhpour@gmail.com"
                    },
                    License = new License {Name = "Private License"}
                });
                //Setting up Swagger (ASP.NET Core) using the Authorization headers (Bearer)
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ShopPromotionApi.xml");
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });
        }
    }
}