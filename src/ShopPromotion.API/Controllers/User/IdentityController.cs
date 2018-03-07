// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopPromotion.Domain.Infrastructure.DAL;

namespace ShopPromotion.API.Controllers.User
{
    // API
    using Services;
    using Infrastructure.Models.ActionResults;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Form;
    using ServiceConfiguration;
    // Domain
    using Domain.EntityLayer;
    using Domain.Services.PaginationHelper;
    // Helper
    using Helper.Infrastructure.ActionResults;

    /// <summary>
    /// Identity controller.
    /// </summary>
    [Area("User")]
    public class IdentityController : BaseController
    {
        private readonly UserManager<BaseIdentityUser> _userManager;
        private readonly ISmsSender _smsSender;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="defaultPagingOptionsAccessor"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="userManager"></param>
        /// <param name="smsSender"></param>
        public IdentityController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork,
            UserManager<BaseIdentityUser> userManager, ISmsSender smsSender) : base(defaultPagingOptionsAccessor,
            unitOfWork)
        {
            _userManager = userManager;
            _smsSender = smsSender;
        }

        /// <summary>
        /// Signin to application with mobile phone as an AppUser.
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
        [HttpPost("Register/AppUser")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegisterViewModel), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> RegisterAppUser([FromBody] RegisterFormModel formModel)
        {
            // Create or Update user.
            var user = await RegisterBaseIdentityUserAsync(formModel, UserClaimOptions.AppUserClaim);
            // Response
            var claim = await _userManager.GetClaimsAsync(user);
            var claimUser = Mapper.Map<IList<MinimumClaimResource>>(claim);
            return Created("created",
                new RegisterViewModel {PhoneNumber = formModel.PhoneNumber, Claim = claimUser});
        }

        /// <summary>
        /// Signin to application with mobile phone as a ShopKeeper.
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
        [HttpPost("Register/ShopKeeper")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegisterViewModel), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> RegisterShopKeeperUser([FromBody] RegisterFormModel formModel)
        {
            // Create or Update user.
            var user = await RegisterBaseIdentityUserAsync(formModel, UserClaimOptions.ShopKeeperClaim);
            // Response
            var claim = await _userManager.GetClaimsAsync(user);
            var claimUser = Mapper.Map<IList<MinimumClaimResource>>(claim);
            return Created("created",
                new RegisterViewModel {PhoneNumber = formModel.PhoneNumber, Claim = claimUser});
        }

        /// <summary>
        /// Create or update user and assign requested claim.
        /// </summary>
        /// <param name="formModel"></param>
        /// <param name="userClaimOptions">The target claim that you want to assing to the new user.</param>
        /// <returns></returns>
        private async Task<BaseIdentityUser> RegisterBaseIdentityUserAsync(RegisterFormModel formModel,
            UserClaimOptions userClaimOptions)
        {
            BaseIdentityUser user;
            try
            {
                // Create user
                user = await UnitOfWork.ShopPromotionUserManager.CreateByPhoneNumberAsync(formModel.PhoneNumber);
                // Generate new verification code for user
                await UnitOfWork.ShopPromotionUserManager.GenerateVerificationCodeAsync(user);
                // Send SMS for verification code
                await _smsSender.SendSmsAsync(user.PhoneNumber, user.VerificationCode);
            }
            catch (DbUpdateException ex)
            {
                // The user exists and duplicate ky exception was thrown. then we just publish and event
                // Get old user.
                user = await UnitOfWork.ShopPromotionUserManager.FindByPhoneAsync(formModel.PhoneNumber);
                // Generate new verification code for user
                await UnitOfWork.ShopPromotionUserManager.GenerateVerificationCodeAsync(user);
                // Send SMS for verification code
                await _smsSender.SendSmsAsync(user.PhoneNumber, user.VerificationCode);
            }

            // Add user claim
            switch (userClaimOptions)
            {
                case UserClaimOptions.AppUserClaim:
                    await _userManager.AddClaimAsync(user,
                        new Claim(ConfigurePolicyService.ClaimType, ConfigurePolicyService.AppUserClaimVelue));
                    break;
                case UserClaimOptions.ShopKeeperClaim:
                    await _userManager.AddClaimAsync(user,
                        new Claim(ConfigurePolicyService.ClaimType, ConfigurePolicyService.ShopKeeperUserClaimVelue));
                    break;
                default:
                    throw new NotImplementedException("Requested claim was not supported!");
            }

            return user;
        }
    }
}