// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopPromotion.API.Extentions;

namespace ShopPromotion.API.Controllers
{
    using Services;
    using Infrastructure.ActionResults;
    using Helper.Infrastructure.ActionResults;
    using Infrastructure.Models;
    using Infrastructure.Models.AccountFormModels;
    using ServiceConfiguration;
    using Domain.EntityLayer;
    using Domain.ModelLayer.Response.Pagination;

    /// <summary>
    /// Identity controller.
    /// </summary>
    public class IdentityController : BaseController
    {
        private readonly UserManager<BaseIdentityUser> _userManager;
        private readonly IShopPromotionUserManager _shopPromotionUserManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="defaultPagingOptionsAccessor"></param>
        /// <param name="userManager"></param>
        /// <param name="shopPromotionUserManager"></param>
        public IdentityController(
            IOptions<PagingOptions> defaultPagingOptionsAccessor,
            UserManager<BaseIdentityUser> userManager,
            IShopPromotionUserManager shopPromotionUserManager
            ) : base(defaultPagingOptionsAccessor)
        {
            _userManager = userManager;
            _shopPromotionUserManager = shopPromotionUserManager;
        }

        /// <summary>
        /// Signin to application with mobile phone.
        /// </summary>
        /// <remarks>
        /// -  If user was not exists, We create new one.
        /// -  The username, email, and the pawwrod was generated randomly.
        /// -  Based claim that assign to created user as default is: `AppUser`.
        /// </remarks>
        /// <param name="formModel"></param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="204">No Content, The user was existed in the database</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegisterViewModel), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> Register([FromBody] RegisterFormModel formModel)
        {
            BaseIdentityUser user;
            try
            {
                // Create user
                user = await _shopPromotionUserManager.CreateByPhoneNumberAsync(formModel.PhoneNumber);
                await _shopPromotionUserManager.GenerateVerificationCodeAsync(user);
            }
            catch (DbUpdateException ex)
            { // The user exists and duplicate ky exception was thrown. then we just publish and event
                user = await _shopPromotionUserManager.FindByPhoneAsync(formModel.PhoneNumber);
                await _shopPromotionUserManager.GenerateVerificationCodeAsync(user);
                return NoContent();
            }

            // Add user claim
            await _userManager.AddClaimAsync(user,
                new Claim(ConfigurePolicyService.ClaimType, ConfigurePolicyService.AppUserClaimVelue));

            // Response
            var claim = await _userManager.GetClaimsAsync(user);
            var claimUser = Mapper.Map<IList<MinimumClaimResource>>(claim);
            return Created("created",
                new RegisterViewModel {PhoneNumber = formModel.PhoneNumber, Claim = claimUser});
        }
    }
}