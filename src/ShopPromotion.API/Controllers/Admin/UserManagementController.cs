// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ShopPromotion.API.Controllers.Admin
{
    // API
    using ServiceConfiguration;
    using Services;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Resource;
    // Domain
    using Domain.ModelLayer.Response.Pagination;
    using Domain.ModelLayer.Response;
    using Domain.EntityLayer;
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
            IOptions<PagingOptions> defaultPagingOptionsAccessor,
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
        [ProducesResponseType(typeof(ApiError), 404)]
        public async Task<IActionResult> GetUsersAsync([FromQuery] PagingOptions page)
        {
            var result = await _shopPromotionUserManager.GetAllUsersAsync();
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
            var users = await _baseIdentityUserManager.FindByEmailAsync(itemByUserNameParameter.UserName);
            var minmized = new MinimumIdentityUserResource
            {
                Id = users.Id,
                UserName = users.UserName
            };
            var response =
                new SingleModelResponse<MinimumIdentityUserResource>() as
                    ISingleModelResponse<MinimumIdentityUserResource>;
            response.Model = minmized;
            if (response.Model == null)
                return NotFound();
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
        public async Task<IActionResult> UpdateUser(GetItemByUserNameParameters itemByUserNameParameters,
            [FromBody] UpdateUserFormModel model)
        {
            var user = await _baseIdentityUserManager.FindByEmailAsync(itemByUserNameParameters.UserName);
            if (user == null) return NotFound();
            await _baseIdentityUserManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            return NoContent();
        }

        /// <summary>
        /// Add employee claim to existing user.
        /// </summary>
        /// <remarks>
        /// #### Supported claim values:
        /// `GarmentMaker`, `Tailor`
        /// #### GarmentMaker:
        /// Known as `Founder` in system and can access to all APIs.
        /// #### Tailor:
        /// Known as `Staff` in system and just can access to tailor related APIs e.g size.
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
        public async Task<IActionResult> AddEmployeeClaim([FromBody] AddClaimFormModel claimFormModel)
        {
            var user = await _baseIdentityUserManager.FindByEmailAsync(claimFormModel.Email);

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
        public async Task<IActionResult> DeleteUser(GetItemByUserNameParameters itemByUserNameParameters)
        {
            var user = await _baseIdentityUserManager.FindByEmailAsync(itemByUserNameParameters.UserName);
            if (user == null) return NotFound();
            await _baseIdentityUserManager.DeleteAsync(user);
            return NoContent();
        }
    }
}