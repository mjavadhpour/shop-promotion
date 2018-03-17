// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ShopPromotion.API.Infrastructure.Data
{
    using AppSettings;
    using Domain.EntityLayer;
    using Domain.Infrastructure;
    using ServiceConfiguration;

    /// <summary>
    /// Create administrator.
    /// </summary>
    public class DbInitializer : IDbInitializer
    {
        private readonly AdministratorOptions _administratorOptions;
        private readonly ShopPromotionDomainContext _context;
        private readonly UserManager<BaseIdentityUser> _userManager;

        /// <summary>
        /// DbInitializer constructor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="appSettings"></param>
        public DbInitializer(ShopPromotionDomainContext context, UserManager<BaseIdentityUser> userManager,
            IOptions<ShopPromotionApiAppSettings> appSettings)
        {
            _context = context;
            _userManager = userManager;
            _administratorOptions = appSettings.Value.AdministratorOptions;
        }

        /// <inheritdoc />
        public async void Initialize()
        {
            // Create database schema if none exists with reference to all migrations.
            await _context.Database.EnsureCreatedAsync();

            // If there is already an Administrator claim, delete old user
            if (_context.UserClaims.Any(x => x.ClaimValue == ConfigurePolicyService.AdminUserClaimVelue))
            {
                var adminClaim =
                    _context.UserClaims.FirstOrDefault(x => x.ClaimValue == ConfigurePolicyService.AdminUserClaimVelue);
                if (adminClaim == null) return;
                var administrator = await _userManager.FindByIdAsync(adminClaim.UserId);
                administrator.UserName = _administratorOptions.UserName;
                administrator.Email = _administratorOptions.Email;
                administrator.VerificationCode = _administratorOptions.VerificationCode;
                administrator.FirstName = "Admin";
                administrator.LastName = "Application";
                administrator.ProfileImagePath = "http://google.com";
                administrator.PhoneNumber = "00000000000";
                administrator.EmailConfirmed = true;
                await _userManager.UpdateAsync(administrator);
            }
            else
            {
                // Create the default Admin account and apply the Administrator role
                await _userManager.CreateAsync(
                    new AdminUser
                    {
                        UserName         = _administratorOptions.UserName, 
                        Email            = _administratorOptions.Email, 
                        VerificationCode = _administratorOptions.VerificationCode, 
                        FirstName = "Admin",
                        LastName = "Application",
                        ProfileImagePath = "http://google.com",
                        PhoneNumber = "00000000000",
                        EmailConfirmed   = true
                    },
                    _administratorOptions.Password);
                var claim = new Claim(ConfigurePolicyService.ClaimType, ConfigurePolicyService.AdminUserClaimVelue);
                await _userManager.AddClaimAsync(await _userManager.FindByNameAsync(_administratorOptions.Email), claim);
            }

            SeedData();
        }

        private async void SeedData()
        {
            // Memory section.
            var adminUser = await _userManager.FindByEmailAsync(_administratorOptions.Email);
            var shopKeeper = new ShopKeeperUser
            {
                Email = "shopkeeper@shoppromotion.com",
                FirstName = "shop",
                LastName = "Keeper",
                PhoneNumber = "1111111111",
                ProfileImagePath = "http://www.google.com",
                UserName = "shopkeeper",
                EmailConfirmed = true,
                CreatedAt = DateTime.Now
            };
            var appUser = new AppUser
            {
                Email = "appuser@shoppromotion.com",
                FirstName = "App",
                LastName = "User",
                PhoneNumber = "2222222222",
                ProfileImagePath = "http://www.google.com",
                UserName = "appuser",
                EmailConfirmed = true,
                CreatedAt = DateTime.Now
            };
            var paymentMethod = new PaymentMethod
            {
                Code = "XXK-G346436",
                GatewayConfig = new GatewayConfig
                {
                    Config = "The config",
                    GatewayName = "Shaparak test"
                },
                IsEnabled = true,
                Position = 1,
                CreatedAt = DateTime.Now
            };
            var attribute = new Attribute
            {
                Description = "Base shop",
                Name = "Base",
                CreatedAt = DateTime.Now
            };
            var shopAddress = new ShopAddress
            {
                Address = "Tehran",
                PhoneNumber = "0000000000",
                CreatedAt = DateTime.Now
            };
            var shop = new Shop
            {
                Facebook = "http:www.facebook.com",
                Instagram = "http://instagram.com",
                Owner = shopKeeper,
                ShopAddresses = new List<ShopAddress>
                {
                    shopAddress
                },
                ShopGeolocation = new ShopGeolocation
                {
                    Latitude = 32.2354657,
                    Longitude = 57.465768,
                    CreatedAt = DateTime.Now
                },
                ShopImages = new List<ShopImage>
                {
                    new ShopImage
                    {
                        Path = "http://google.com",
                        Type = "none",
                        CreatedAt = DateTime.Now
                    }
                },
                ShopStatuses = new List<ShopStatus>
                {
                    new ShopStatus
                    {
                        Status = ShopStatusOption.Disapproved,
                        CreatedAt = DateTime.Now
                    }
                },
                Telegram = "http:www.telegram.com",
                Title = "KFC",
                Twitter = "http://www.twitter.com",
                CreatedAt = DateTime.Now
            };
            var promotion = new ShopPromotion
            {
                AverageRating = 5,
                Description = "This is test promotion",
                DiscountPercent = 10,
                EntAt = DateTime.Now,
                Shop = shop,
                Name = "First promotion",
                MinimumPaymentAmount = 20000,
                StartAt = DateTime.Now,
                PaymentMethod = paymentMethod,
                UsageLimit = 100,
                Used = 1,
                CreatedAt = DateTime.Now
            };
            var shopPromotionBarcode = new ShopPromotionBarcode
            {
                Promotion = promotion,
                Barcode = new Guid().ToString(),
                CreatedAt = DateTime.Now
            };
            var shopAttribute = new ShopAttribute
            {
                Attribute = attribute,
                Shop = shop,
                CreatedAt = DateTime.Now
            };
            var order = new Order
            {
                Code = new Guid(),
                CheckoutCompletedAt = DateTime.Now,
                CheckoutState = "Confirmed",
                Customer = appUser,
                ItemsTotal = 1,
                State = "Confirmed",
                PaymentState = "Approved",
                Notes = "This is test order",
                Total = 1000000,
                ShopPromotionBarcode = shopPromotionBarcode,
                CreatedAt = DateTime.Now
            };
            var specialOffer = new SpecialOffer
            {
                Shop = shop,
                ExpireAt = DateTime.Now,
                IsEnabled = true,
                Description = "This is a test special offer."
            };

            // Database section.
            if (!_context.Attributes.Any())
            {
                _context.Attributes.Add(attribute);
                await _context.SaveChangesAsync();
            }

            if (!_context.Messages.Any())
            {
                _context.Messages.Add(new Message
                {
                    Author = adminUser as AdminUser,
                    MessageTargets = new List<MessageTarget>
                    {
                        new MessageTarget
                        {
                            TargetObjectId = "2",
                            TargetType = MessageTargetTypeOption.Shop
                        }
                    },
                    Note = "Welcome to the application.",
                    Subject = "Welcome"
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Shops.Any())
            {
                shop.ShopAttributes = new List<ShopAttribute>
                {
                    shopAttribute
                };
                _context.Shops.Add(shop);
                await _context.SaveChangesAsync();
            }

            if (!_context.Orders.Any())
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            if (!_context.SpecialOffers.Any())
            {
                _context.SpecialOffers.Add(specialOffer);
                await _context.SaveChangesAsync();
            }
        }
    }
}