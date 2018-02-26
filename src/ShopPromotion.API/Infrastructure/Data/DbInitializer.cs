// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ShopPromotion.API.Infrastructure.Data
{
    using AppSettings;
    using Domain.EntityLayer;
    using Domain.Infrastructure;

    /// <summary>
    /// Create administrator.
    /// </summary>
    public class DbInitializer : IDbInitializer
    {
        private readonly AdministratorOptions _administratorOptions;
        private readonly ShopPromotionDomainContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// DbInitializer constructor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="appSettings"></param>
        public DbInitializer(ShopPromotionDomainContext context, UserManager<ApplicationUser> userManager,
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
            if (_context.UserClaims.Any(x => x.ClaimValue == "Administrator"))
            {
                var adminClaim = _context.UserClaims.FirstOrDefault(x => x.ClaimValue == "Administrator");
                if (adminClaim != null)
                {
                    var administrator = await _userManager.FindByIdAsync(adminClaim.UserId);
                    await _userManager.DeleteAsync(administrator);
                }
            }

            // Create the default Admin account and apply the Administrator role
            var userName = _administratorOptions.UserName;
            var email = _administratorOptions.Email;
            var password = _administratorOptions.Password;
            await _userManager.CreateAsync(new ApplicationUser {UserName = userName, Email = email, EmailConfirmed = true},
                password);
            var claim = new Claim("Role", "Administrator");
            await _userManager.AddClaimAsync(await _userManager.FindByNameAsync(email), claim);
        }
    }
}