// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>


using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ShopPromotion.API.Services
{
    using Infrastructure.AppSettings;

    /// <summary>
    /// Create token with registered configuration.
    /// </summary>
    public class TokenProviderService
    {
        private readonly TokenOptions _tokenOptions;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="appSettings"></param>
        public TokenProviderService(IOptions<ShopPromotionApiAppSettings> appSettings)
        {
            _tokenOptions = appSettings.Value.TokenOptions;
        }

        /// <summary>
        /// Create token.
        /// </summary>
        /// <param name="userClaims"></param>
        /// <returns></returns>
        public JwtSecurityToken CreateToken(IList<Claim> userClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                userClaims,
                expires: DateTime.Now.AddMinutes(_tokenOptions.ExpiresTimeAsMinutes),
                signingCredentials: creds);
        }
    }
}