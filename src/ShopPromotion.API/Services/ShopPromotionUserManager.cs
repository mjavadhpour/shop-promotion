// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
    using Domain.Extensions;
    using Domain.Infrastructure.AppSettings;

    /// <inheritdoc />
    public class ShopPromotionUserManager : IShopPromotionUserManager
    {
        private readonly ShopPromotionDomainContext _context;
        private readonly UserManager<BaseIdentityUser> _userManager;
        private readonly IPagingOptions _defaultPagingOptions;
        private readonly IQueryable<BaseIdentityUser> _queryableIdentityUsers;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="shopPromotionDomainContext"></param>
        /// <param name="userManager"></param>
        /// <param name="shopPromotionDomainOptions"></param>
        /// <param name="domainContext"></param>
        public ShopPromotionUserManager(
            ShopPromotionDomainContext shopPromotionDomainContext,
            UserManager<BaseIdentityUser> userManager,
            IOptions<ShopPromotionDomainAppSettings> shopPromotionDomainOptions,
            ShopPromotionDomainContext domainContext)
        {
            _context = shopPromotionDomainContext;
            _userManager = userManager;
            _defaultPagingOptions = shopPromotionDomainOptions.Value.PagingOptions;
            _queryableIdentityUsers = domainContext.Set<BaseIdentityUser>();
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
        public Task<Page<MinimumIdentityUserResource>> GetAllUsersAsync(PagingOptions pagingOptions)
        {
            return Task.Run(() => GetAllUsers(pagingOptions));
        }

        /// <summary>
        /// Paginate and get all users.
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns></returns>
        private async Task<Page<MinimumIdentityUserResource>> GetAllUsers(PagingOptions pagingOptions)
        {
            var pageSize = pagingOptions.GetTakeValue(pagingOptions, _defaultPagingOptions);
            var pageNumber = pagingOptions.GetSkipValue(pagingOptions, _defaultPagingOptions);
            var orderBy = pagingOptions.GetOrderBy(pagingOptions, _defaultPagingOptions);
            var ascending = pagingOptions.GetAscending(pagingOptions, _defaultPagingOptions);

            var totalNumberOfRecords = await _queryableIdentityUsers.CountAsync(CancellationToken.None);           

            // Please add user claim with the help of this issue: https://github.com/aspnet/Identity/issues/1361
            var users = await _userManager.Users
                .OrderByPropertyOrField(orderBy, ascending) // Order
                .Skip(pageNumber * pageSize) // Offset
                .Take(pageSize) // Limit
                .ProjectTo<MinimumIdentityUserResource>() // Auto mapping
                .ToArrayAsync(CancellationToken.None);

            return Page<MinimumIdentityUserResource>.Create(users, totalNumberOfRecords,
                _defaultPagingOptions);
        }
    }
}