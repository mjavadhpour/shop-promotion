// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers
{
    // ShopPromotion API
    using Services;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.ActionResults;
    // ShopPromotion Domain
    using Domain.EntityLayer;
    using Domain.Services.PaginationHelper;
    // ShopPromotion Helper
    using Helper.Infrastructure.ActionResults;
    using Helper.Infrastructure.Filters;

    /// <summary>
    /// Connect controller.
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ConnectController : BaseController
    {
        private readonly UserManager<BaseIdentityUser> _userManager;
        private readonly IShopPromotionUserManager _shopPromotionUserManager;
        private readonly TokenProviderService _tokenProviderService;

        /// <inheritdoc />
        public ConnectController(
            ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UserManager<BaseIdentityUser> userManager, 
            IShopPromotionUserManager shopPromotionUserManager,
            TokenProviderService tokenProviderService)
            : base(defaultPagingOptionsAccessor)
        {
            _userManager = userManager;
            _shopPromotionUserManager = shopPromotionUserManager;
            _tokenProviderService = tokenProviderService;
        }

        /// <summary>
        ///A JWT access token.
        /// </summary>
        /// <param name="loginFormModel"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("token")]
        [ValidateModel]
        [ProducesResponseType(typeof(TokenViewModel), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        // TODO: Add more situation handlling.
        public async Task<IActionResult> Token([FromBody] LoginFormModel loginFormModel)
        {
            var user = await _shopPromotionUserManager.FindByCodeAsync(loginFormModel.Code);

            if (user == null) return BadRequest(new ApiError("Could not create token"));

            var userClaims = await _userManager.GetClaimsAsync(user);

            var token = _tokenProviderService.CreateToken(userClaims);

            return Ok(new TokenViewModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = token.ValidTo,
                Claim = Mapper.Map<IList<MinimumClaimResource>>(userClaims)
            });
        }
    }
}