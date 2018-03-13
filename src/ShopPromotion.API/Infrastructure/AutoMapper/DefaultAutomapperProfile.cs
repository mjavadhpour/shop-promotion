// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Security.Claims;
using AutoMapper;

namespace ShopPromotion.API.Infrastructure.AutoMapper
{
    // API
    using Models.Resource;
    using Models.Form;
    // Domain
    using Domain.EntityLayer;
    using Domain.Infrastructure.Models.Resource;

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
            // Shop
            CreateMap<ShopForm, Shop>();
            CreateMap<ShopForm, MinimumShopResource>();
            // ShopPromotion
            CreateMap<ShopPromotionForm, ShopPromotion>();
            CreateMap<ShopPromotionForm, MinimumShopPromotionResource>();
        }
    }
}