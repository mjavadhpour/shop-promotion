// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.Admin
{
    // API
    using ServiceConfiguration;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Parameter;
    // Domain
    using Domain.Infrastructure.Models.Resource.Custom;
    using Domain.Infrastructure.Models.Response;
    using Domain.Infrastructure.Models.Response.Pagination;
    using Domain.Services;
    using Domain.EntityLayer;
    using Domain.Services.PaginationHelper;
    // Helper
    using Helper.Infrastructure.ActionResults;

    /// <summary>
    /// Shop controller.
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class UserManagementController : BaseController
    {
        private readonly IShopPromotionUserManager _shopPromotionUserManager;
        private readonly UserManager<BaseIdentityUser> _baseIdentityUserManager;

        /// <inheritdoc />
        public UserManagementController(
            ResolvedPaginationValueService defaultPagingOptionsAccessor,
            IShopPromotionUserManager shopPromotionUserManager,
            UserManager<BaseIdentityUser> baseIdentityUserManager) : base(
            defaultPagingOptionsAccessor)
        {
            _shopPromotionUserManager = shopPromotionUserManager;
            _baseIdentityUserManager = baseIdentityUserManager;
        }

        /// <summary>
        /// Get All users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [ProducesResponseType(typeof(Page<MinimumIdentityUserResource>), 200)]
        public async Task<IActionResult> GetUsersAsync([FromQuery] PagingOptions pagingOptions)
        {
            var result = await _shopPromotionUserManager.GetAllUsersAsync(pagingOptions);
            return Ok(result);
        }

        /// <summary>
        /// Get special user.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{UserName}")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumIdentityUserResource>), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> GetUserAsync(GetItemByUserNameParameters itemByUserNameParameter)
        {
            var user = await _baseIdentityUserManager.FindByEmailAsync(itemByUserNameParameter.UserName);
            var response =
                new SingleModelResponse<MinimumIdentityUserResource>() as
                    ISingleModelResponse<MinimumIdentityUserResource>;
            response.Model = Mapper.Map<MinimumIdentityUserResource>(user);
            // 404
            if (response.Model == null) return NotFound();
            return Ok(response.Model);
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <remarks>
        /// change the user password
        /// </remarks>
        /// <param name="itemByUserNameParameters"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Updated</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{UserName}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> UpdateUserAsync(GetItemByUserNameParameters itemByUserNameParameters,
            [FromBody] UpdateUserFormModel model)
        {
            var user = await _baseIdentityUserManager.FindByEmailAsync(itemByUserNameParameters.UserName);
            // 404
            if (user == null) return NotFound();
            await _baseIdentityUserManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            return NoContent();
        }

        /// <summary>
        /// Add employee claim to existing user.
        /// </summary>
        /// <remarks>
        /// #### Supported claim values:
        /// `AppUser`, `ShopKeeperUser`
        /// ----
        /// **Note:** Eeach users can just have one claim.
        /// </remarks>
        /// <returns></returns>
        /// <response code="204">Added</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("claim")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> AddEmployeeClaimAsync([FromBody] AddClaimFormModel claimFormModel)
        {
            var user = await _baseIdentityUserManager.FindByEmailAsync(claimFormModel.Email);
            // 404
            if (user == null) return NotFound();
            // Remove previous claims.
            var userClaims = await _baseIdentityUserManager.GetClaimsAsync(user);
            await _baseIdentityUserManager.RemoveClaimsAsync(user, userClaims);

            var claim = new Claim(ConfigurePolicyService.ClaimType, claimFormModel.Value);

            await _baseIdentityUserManager.AddClaimAsync(user, claim);

            return NoContent();
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Removed</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{UserName}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> DeleteUserAsync(GetItemByUserNameParameters itemByUserNameParameters)
        {
            var user = await _baseIdentityUserManager.FindByEmailAsync(itemByUserNameParameters.UserName);
            // 404
            if (user == null) return NotFound();
            await _baseIdentityUserManager.DeleteAsync(user);
            return NoContent();
        }
    }
}