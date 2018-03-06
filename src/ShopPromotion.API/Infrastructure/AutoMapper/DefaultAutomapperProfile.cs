// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Security.Claims;
using AutoMapper;

namespace ShopPromotion.API.Infrastructure.AutoMapper
{
    using Models.Resource;
    // Domain
    using Domain.EntityLayer;
    using Domain.Infrastructure.Models.Resource.Custom;

    /// <summary>
    /// Default auto mapping configuration and define supported mapping in project.
    /// </summary>
    public class DefaultAutomapperProfile : Profile
    {
        /// <summary>
        /// Add all auto mapp here.
        /// </summary>
        public DefaultAutomapperProfile()
        {
            // Claim
            CreateMap<Claim, MinimumClaimResource>();
            // Application User
            CreateMap<BaseIdentityUser, MinimumIdentityUserResource>();
            CreateMap<MinimumIdentityUserResource, BaseIdentityUser>();
            CreateMap<MinimumIdentityUserResource, MinimumIdentityUserResource>();
        }
    }
}