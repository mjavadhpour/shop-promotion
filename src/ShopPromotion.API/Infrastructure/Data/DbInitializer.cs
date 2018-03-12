// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

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
            var adminUser = await _userManager.FindByEmailAsync(_administratorOptions.Email);

            if (!_context.Attributes.Any())
            {
                _context.Attributes.Add(new Attribute
                {
                    Description = "Base shop",
                    Name = "Base"
                });
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
        }
    }
}