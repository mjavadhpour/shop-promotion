// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ShopPromotion.Domain.Infrastructure
{
    using EntityLayer;
    using AppSettings;

    /// <inheritdoc />
    public class ShopPromotionDomainContext : IdentityDbContext<ApplicationUser>
    {
        public ShopPromotionDomainContext(DbContextOptions<ShopPromotionDomainContext> options,
            IOptions<ShopPromotionDomainAppSettings> appSettings) : base(options)
        {
            ConnectionString = appSettings.Value.ConnectionStrings.DefaultConnection;
        }

        public string ConnectionString { get; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        /// <summary>
        /// Top of all confiuration for context. all things can be override here.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Override OnModelCreating fiunction (using fluent API)
        /// <see href="https://docs.microsoft.com/en-us/ef/core/modeling/" />
        /// </summary>
        /// <remarks>
        /// Customize the ASP.NET Identity model and override the defaults if needed.
        /// For example, you can rename the ASP.NET Identity table names and more.
        /// Add your customizations after calling base.OnModelCreating(builder);
        /// </remarks>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            // Execute system onModelCreating to handle other library database.
            base.OnModelCreating(modelBuilder);
        }
    }
}