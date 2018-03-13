// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;

namespace ShopPromotion.Domain.Services.UserManager
{
    using EntityLayer;
    using Infrastructure.Models.Response.Pagination;
    using Infrastructure.Models.Resource.Custom;

    /// <summary>
    /// Custom user manager for support functionallity of ShopPromotion login API.
    /// </summary>
    public interface IShopPromotionUserManager
    {
        /// <summary>
        /// Gets the user, if any, associated with the value of the specified phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the user, if any, associated with a value of the specified phone number.
        /// </returns>
        Task<BaseIdentityUser> FindByPhoneAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the user, if any, associated with the value of the specified verification code.
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the user, if any, associated with a value of the specified phone number.
        /// </returns>
        Task<BaseIdentityUser> FindByCodeAsync(string verificationCode, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates the specified user in the backing store with given <paramref name="phoneNumber" />,
        /// Create a user with requested phone number. The email, username was generated randomly.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        Task<BaseIdentityUser> CreateByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// Generate verification code for given <paramref name="user" />.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
        /// of the operation.
        /// </returns>
        Task<int> GenerateVerificationCodeAsync(BaseIdentityUser user);

        /// <summary>
        /// Get all idnetity users with pagination.
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns></returns>
        Task<Page<MinimumIdentityUserResource>> GetAllUsersAsync(PagingOptions pagingOptions);
    }
}