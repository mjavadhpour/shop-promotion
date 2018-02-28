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
    public class ShopPromotionDomainContext : IdentityDbContext<BaseIdentityUser>
    {
        public ShopPromotionDomainContext(DbContextOptions<ShopPromotionDomainContext> options,
            IOptions<ShopPromotionDomainAppSettings> appSettings) : base(options)
        {
            DefaultConnectionString = appSettings.Value.ConnectionStrings.DefaultConnection;
            MySqlConnectionString = appSettings.Value.ConnectionStrings.MySqlConnection;
        }

        private string DefaultConnectionString { get; }
        private string MySqlConnectionString { get; }

        public DbSet<AdminUser> AdminUsers { get; set; }
        
        public DbSet<ShopKeeperUser> ShopKeeperUsers{ get; set; }
        
        public DbSet<AppUser> AppUsers { get; set; }
        
        public DbSet<Shop> Shops { get; set; }
        
        public DbSet<ShopReview> ShopReviews { get; set; }
        
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        
        public DbSet<ShopStatus> ShopStatuses { get; set; }

        public DbSet<ShopImage> ShopImages { get; set; }
        
        public DbSet<ShopPromotion> ShopPromotions { get; set; }
        
        public DbSet<PromotionBarcode> PromotionBarcodes { get; set; }

        public DbSet<ShopGeolocation> ShopGeolocations { get; set; }
        
        public DbSet<ShopAttribute> ShopAttributes { get; set; }
        
        public DbSet<Attribute> Attributes { get; set; }
        
        /// <summary>
        /// Top of all confiuration for context. all things can be override here.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(MySqlConnectionString);

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