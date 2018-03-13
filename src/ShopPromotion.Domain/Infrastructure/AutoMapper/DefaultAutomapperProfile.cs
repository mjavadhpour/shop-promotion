// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using AutoMapper;

namespace ShopPromotion.Domain.Infrastructure.AutoMapper
{
    using EntityLayer;
    using Models.Resource;
    using Models.Resource.Custom;

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
            // Application User
            CreateMap<BaseIdentityUser, MinimumIdentityUserResource>()
                .ForMember(dto => dto.FullName, opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"));
            CreateMap<MinimumIdentityUserResource, BaseIdentityUser>();
            CreateMap<MinimumIdentityUserResource, MinimumIdentityUserResource>();
            // Shop
            CreateMap<Shop, MinimumShopListResource>()
                .ForMember(dto => dto.Image,
                    opt => { opt.MapFrom(y => y.ShopImages.Select(img => img.Path).FirstOrDefault()); })
                .ForMember(dto => dto.Attributes, opt => opt.MapFrom(s => s.ShopAttributes.Select(a => a.Attribute)));
            CreateMap<Shop, MinimumShopResource>()
                .ForMember(dto => dto.Attributes, opt => opt.MapFrom(y => y.ShopAttributes.Select(a => a.Attribute)))
                .ForMember(dto => dto.Geolocation, opt => opt.MapFrom(y => y.ShopGeolocation))
                .ForMember(dto => dto.Status,
                    opt => opt.MapFrom(y => y.ShopStatuses.Select(s => s.Status).LastOrDefault()))
                .ForMember(dto => dto.Images,
                    opt => opt.MapFrom(y => y.ShopImages));
            CreateMap<ShopAttribute, MinimumAttributeResource>();
            // ShopPromotion
            CreateMap<ShopPromotion, MinimumShopPromotionResource>();
            // ShopImage
            CreateMap<ShopImage, MinimumShopImageResource>();
            // ShopGeolocations
            CreateMap<ShopGeolocation, MinimumShopGeolocationsResource>();
            // Message
            CreateMap<Message, MinimumMessageResource>();
            CreateMap<Message, MinimumMessageListResource>();
            // SpecialOffer
            CreateMap<SpecialOffer, MinimumSpecialOfferResource>();
            // Attribute
            CreateMap<Attribute, MinimumAttributeResource>();
            // Order
            CreateMap<Order, MinimumOrderResource>();
        }
    }
}