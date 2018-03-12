// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public DbSet<BaseIdentityUser> BaseIdentityUsers { get; set; }

        public DbSet<AdminUser> AdminUsers { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppUserInbox> AppUserInboxes { get; set; }

        public DbSet<AppUserPrivilege> AppUserPrivileges { get; set; }

        public DbSet<Attribute> Attributes { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<DiscountCoupon> DiscountCoupons { get; set; }

        public DbSet<GatewayConfig> GatewayConfigs { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageTarget> MessageTargets { get; set; }

        public DbSet<Privilege> Privileges { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<ShopAddress> ShopAddresses { get; set; }

        public DbSet<ShopAttribute> ShopAttributes { get; set; }

        public DbSet<ShopGeolocation> ShopGeolocations { get; set; }

        public DbSet<ShopImage> ShopImages { get; set; }

        public DbSet<ShopInbox> ShopInboxes { get; set; }

        public DbSet<ShopKeeperUser> ShopKeeperUsers{ get; set; }

        public DbSet<ShopKeeperUserInbox> ShopKeeperUserInboxes { get; set; }

        public DbSet<ShopPromotion> ShopPromotions { get; set; }

        public DbSet<ShopPromotionBarcode> PromotionBarcodes { get; set; }

        public DbSet<ShopPromotionReview> ShopReviews { get; set; }

        public DbSet<ShopStatus> ShopStatuses { get; set; }

        public DbSet<SpecialOffer> SpecialOffers { get; set; }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        /// <inheritdoc />
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <inheritdoc />
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Top of all confiuration for context. all things can be override here.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseMySql(MySqlConnectionString);
            optionsBuilder.UseSqlServer(DefaultConnectionString);

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
            // Base identity user configurarions.
            modelBuilder.Entity<BaseIdentityUser>(b =>
            {
                b.HasIndex(u => u.PhoneNumber).HasName("PhoneNumberIndex").IsUnique();
            });
            // shop promotion barcode configurarions.
            modelBuilder.Entity<ShopPromotionBarcode>(b =>
            {
                b.HasIndex(u => u.Barcode).HasName("BarcodeIndex").IsUnique();
            });            
            // Execute system onModelCreating to handle other library database.
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Clear tracked entities in entity framework <a href="https://stackoverflow.com/questions/27423059/how-do-i-clear-tracked-entities-in-entity-framework"/>
        /// </summary>
        public void DetachAllEntities()
        {
            var changedEntriesCopy = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();
            foreach (var entity in changedEntriesCopy)
            {
                Entry(entity.Entity).State = EntityState.Detached;
            }
        }

        /// <summary>
        /// Set updated at.
        /// </summary>
        /// <remarks>
        /// The created at filed not work here.
        /// </remarks>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Modified);

            foreach (var entity in entities)
            {
                ((BaseEntity) entity.Entity).UpdatedAt = DateTime.Now;
            }
        }
    }
}