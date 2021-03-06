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

namespace ShopPromotion.Domain.Services.UserManager
{
    using EntityLayer;
    using Extensions;
    using Infrastructure;
    using Infrastructure.Models.Resource.Custom;
    using Infrastructure.Models.Response.Pagination;
    using PaginationHelper;
    using Exceptions;

    /// <inheritdoc />
    public class ShopPromotionUserManager : IShopPromotionUserManager
    {
        private readonly ShopPromotionDomainContext _context;
        private readonly UserManager<BaseIdentityUser> _userManager;
        private readonly IQueryable<BaseIdentityUser> _queryableIdentityUsers;

        /// <summary>
        /// Resolved velue for pagination.
        /// </summary>
        private readonly ResolvedPaginationValueService _paginationValue;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="shopPromotionDomainContext"></param>
        /// <param name="userManager"></param>
        /// <param name="resolvedPaginationValue"></param>
        /// <param name="domainContext"></param>
        public ShopPromotionUserManager(
            ShopPromotionDomainContext shopPromotionDomainContext,
            UserManager<BaseIdentityUser> userManager,
            ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext domainContext)
        {
            _context = shopPromotionDomainContext;
            _userManager = userManager;
            _paginationValue = resolvedPaginationValue;
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
                throw new ArgumentNullException(nameof(phoneNumber));
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
            user.VerificationCode = Extensions.GenerateNewUniqueRandom();
            _context.BaseIdentityUsers.Update(user);
            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task<Page<MinimumIdentityUserResource>> GetAllUsersAsync(PagingOptions pagingOptions)
        {
            return Task.Run(() => GetAllUsers(pagingOptions));
        }

        /// <inheritdoc />
        public async Task<IdentityResult> UpdateAsync(BaseIdentityUser user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.BaseIdentityUsers.Attach(user);
            user.ConcurrencyStamp = Guid.NewGuid().ToString();
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed(new IdentityError());
            }

            return IdentityResult.Success;
        }

        /// <summary>
        /// Paginate and get all users.
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns></returns>
        private async Task<Page<MinimumIdentityUserResource>> GetAllUsers(PagingOptions pagingOptions)
        {
            var totalNumberOfRecords = await _queryableIdentityUsers.CountAsync(CancellationToken.None);

            // Please add user claim with the help of this issue: https://github.com/aspnet/Identity/issues/1361
            var users = await _userManager.Users
                .OrderByPropertyOrField(_paginationValue.OrderBy, _paginationValue.Ascending) // Order
                .Skip(_paginationValue.Page * _paginationValue.PageSize) // Offset
                .Take(_paginationValue.PageSize) // Limit
                .ProjectTo<MinimumIdentityUserResource>() // Auto mapping
                .ToArrayAsync(CancellationToken.None);

            return Page<MinimumIdentityUserResource>.Create(users, totalNumberOfRecords, _paginationValue);
        }
    }
}