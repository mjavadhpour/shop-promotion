// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ShopPromotion.API.Controllers
{
    using Infrastructure.ActionResults;
    using Infrastructure.Models;
    using Infrastructure.Models.AccountFormModels;
    using Infrastructure.AppSettings;
    using TokenOptions = Infrastructure.AppSettings.TokenOptions;
    
    using Domain.EntityLayer;
    using Domain.ModelLayer.Response.Pagination;
    
    using Helper.Infrastructure.ActionResults;
    using Helper.Infrastructure.Filters;

    /// <summary>
    ///Connect controller.
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ConnectController : BaseController
    {
        private readonly SignInManager<BaseIdentityUser> _signInManager;
        private readonly TokenOptions _tokenOptions;
        private readonly UserManager<BaseIdentityUser> _userManager;

        /// <inheritdoc />
        public ConnectController(IOptions<PagingOptions> defaultPagingOptionsAccessor,
            UserManager<BaseIdentityUser> userManager, SignInManager<BaseIdentityUser> signInManager,
            IOptions<ShopPromotionApiAppSettings> appSettings)
            : base(defaultPagingOptionsAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenOptions = appSettings.Value.TokenOptions;
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
        public async Task<IActionResult> Token([FromBody] LoginFormModel loginFormModel)
        {
            var user = await _userManager.FindByEmailAsync(loginFormModel.Email);

            if (user == null) return BadRequest(new ApiError("Could not create token"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginFormModel.Password, false);

            if (!result.Succeeded) return BadRequest(new ApiError("Could not create token"));

            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                userClaims,
                expires: DateTime.Now.AddMinutes(_tokenOptions.ExpiresTimeAsMinutes),
                signingCredentials: creds);

            return Ok(new TokenViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo,
                Claim = Mapper.Map<IList<MinimumClaimResource>>(userClaims)
            });
        }
    }
}