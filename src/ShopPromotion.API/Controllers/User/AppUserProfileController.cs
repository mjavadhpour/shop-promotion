// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopPromotion.API.Infrastructure.Models.Resource;
using ShopPromotion.Domain.EntityLayer;
using ShopPromotion.Domain.Infrastructure.Models.Resource.Custom;

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
    [Area("User")]
    [Route("api/v1/[area]")]
    [Authorize(Policy = ConfigurePolicyService.AppUserPolicy)]
    public class UserProfileController : BaseController
    {
        private readonly IShopPromotionUserManager _shopPromotionUserManager;
        private readonly UserManager<BaseIdentityUser> _userManager;

        /// <inheritdoc />
        public UserProfileController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork, IShopPromotionUserManager shopPromotionUserManager,
            UserManager<BaseIdentityUser> userManager) : base(
            defaultPagingOptionsAccessor, unitOfWork)
        {
            _shopPromotionUserManager = shopPromotionUserManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Edit app user profile.
        /// </summary>
        /// <response code="204">Updated</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("Profile/{PhoneNumber}")]
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

        /// <summary>
        /// Get current logged in user.
        /// </summary>
        /// <response code="200">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("Current")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumIdentityUserResource>), 200)]
        public async Task<IActionResult> UploadAppUserImageAsync()
        {
            // Find requested user.
            var user = await _userManager.GetUserAsync(HttpContext.User);

            // If app user not found
            if (user == null) throw new UserNotFoundException();
            var response =
                new SingleModelResponse<MinimumIdentityUserResource>() as
                    ISingleModelResponse<MinimumIdentityUserResource>;
            response.Model = Mapper.Map<MinimumIdentityUserResource>(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            response.Model.ClaimList = Mapper.Map<IList<MinimumClaimResource>>(userClaims);
            return Ok(response);
        }
    }
}