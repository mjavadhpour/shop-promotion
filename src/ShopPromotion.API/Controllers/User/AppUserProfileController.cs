// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.User
{
    // API
    using ServiceConfiguration;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Parameter;
    // Domain
    using Domain.Services.PaginationHelper;
    using Domain.Infrastructure.DAL;
    using Domain.Exceptions;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response;
    using Domain.Services.UserManager;

    /// <summary>
    /// AppUser image controller.
    /// </summary>
    [Area("AppUser")]
    [Route("api/v1/[area]/Profile")]
    [Authorize(Policy = ConfigurePolicyService.AppUserPolicy)]
    public class UserProfileController : BaseController
    {
        private readonly IShopPromotionUserManager _shopPromotionUserManager;

        /// <inheritdoc />
        public UserProfileController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork, IShopPromotionUserManager shopPromotionUserManager) : base(
            defaultPagingOptionsAccessor, unitOfWork)
        {
            _shopPromotionUserManager = shopPromotionUserManager;
        }

        /// <summary>
        /// Upload an image for app user.
        /// </summary>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{PhoneNumber}")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumAppUserImageResource>), 201)]
        public async Task<IActionResult> UploadAppUserImageAsync(GetItemByPhoneNumberParameters byPhoneNumberParameters,
            [FromBody] UpdateUserFormModel form,
            CancellationToken ct)
        {
            // Find requested app user.
            var appUser =
                await UnitOfWork.ShopPromotionUserManager.FindByPhoneAsync(byPhoneNumberParameters.PhoneNumber, ct);

            // If app user not found
            if (appUser == null) throw new UserNotFoundException();

            appUser.FirstName = form.FirstName;
            appUser.LastName = form.LastName;
            appUser.PhoneNumber = form.PhoneNumber;
            appUser.Email = form.Email;
            appUser.SecurityStamp = Guid.NewGuid().ToString();
            await _shopPromotionUserManager.UpdateAsync(appUser, ct);
            return NoContent();
        }
    }
}