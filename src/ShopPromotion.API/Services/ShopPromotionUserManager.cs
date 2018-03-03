// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.API.Services
{
    // API
    using Exceptions;
    using Extentions;
    using Infrastructure.Models.Resource;
    // Domain
    using Domain.EntityLayer;
    using Domain.Infrastructure;
    using Domain.ModelLayer.Response.Pagination;

    /// <inheritdoc />
    public class ShopPromotionUserManager : IShopPromotionUserManager
    {
        private readonly ShopPromotionDomainContext _context;
        private readonly UserManager<BaseIdentityUser> _userManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="shopPromotionDomainContext"></param>
        /// <param name="userManager"></param>
        public ShopPromotionUserManager(
            ShopPromotionDomainContext shopPromotionDomainContext,
            UserManager<BaseIdentityUser> userManager)
        {
            _context = shopPromotionDomainContext;
            _userManager = userManager;
        }

        /// <inheritdoc />
        public async Task<BaseIdentityUser> FindByPhoneAsync(string phoneNumber,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (phoneNumber == null)
                throw new ArgumentNullException(nameof(phoneNumber));

            return 
                await 
                    Task.FromResult(
                        _context.BaseIdentityUsers
                            .AsNoTracking()
                            .FirstOrDefault(u => u.PhoneNumber == phoneNumber));
        }

        /// <inheritdoc />
        public async Task<BaseIdentityUser> FindByCodeAsync(string verificationCode,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (verificationCode == null)
                throw new ArgumentNullException(nameof(verificationCode));

            return
                await
                    Task.FromResult(
                        _context.BaseIdentityUsers
                            .AsNoTracking()
                            .FirstOrDefault(u => u.VerificationCode == verificationCode));
        }

        /// <inheritdoc />
        public async Task<BaseIdentityUser> CreateByPhoneNumberAsync(string phoneNumber)
        {
            if (phoneNumber == null)
                throw new ArgumentNullException(nameof (phoneNumber));
            // Create new user
            var user = new AppUser
            {
                UserName = Guid.NewGuid().ToString(),
                Email = $@"{Guid.NewGuid().ToString()}@email.com",
                PhoneNumber = phoneNumber
            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                throw new CanNotCreateUserException();

            return user;
        }

        /// <inheritdoc />
        /// <remarks>
        /// Be careful! by running this method the context will be refereshed and lost all registered data.
        /// </remarks>
        public async Task<int> GenerateVerificationCodeAsync(BaseIdentityUser user)
        {
            // Clear tracked entities in entity framework
            _context.DetachAllEntities();
            user.VerificationCode = RandomHelper.GenerateNewUniqueRandom();
            _context.BaseIdentityUsers.Update(user);
            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task<Page<MinimumIdentityUserResource>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}