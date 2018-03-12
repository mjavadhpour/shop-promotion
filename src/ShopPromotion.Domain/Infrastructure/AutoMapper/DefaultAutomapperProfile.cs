// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using AutoMapper;

namespace ShopPromotion.Domain.Infrastructure.AutoMapper
{
    using EntityLayer;
    using Models.Resource;
    using Models.Resource.Custom;
    using Models.Form;

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
            // TODO: Mapping many to many attributes and latest string image path.
            CreateMap<Shop, MinimumShopListResource>()
                .ForMember(dto => dto.Image, opt => opt.UseDestinationValue());
            CreateMap<Shop, MinimumShopResource>()
                .ForMember(dto => dto.Attributes, opt => opt.MapFrom(y => y.ShopAttributes))
                .ForMember(dto => dto.Images, opt => opt.MapFrom(y => y.ShopImages))
                .ForMember(dto => dto.Geolocation, opt => opt.MapFrom(y => y.ShopGeolocation));
            CreateMap<ShopForm, Shop>();
            CreateMap<ShopForm, MinimumShopResource>();
            CreateMap<ShopAttribute, MinimumAttributeResource>();
            // ShopPromotion
            CreateMap<ShopPromotion, MinimumShopPromotionResource>();
            CreateMap<ShopPromotionForm, ShopPromotion>();
            CreateMap<ShopPromotionForm, MinimumShopPromotionResource>();
            // ShopImage
            CreateMap<ShopImage, MinimumShopImageResource>();
            // ShopGeolocations
            CreateMap<ShopGeolocation, MinimumShopGeolocationsResource>();
            // Message
            CreateMap<Message, MinimumMessageResource>();
            // SpecialOffer
            CreateMap<SpecialOffer, MinimumSpecialOfferResource>();
            // Attribute
            CreateMap<Attribute, MinimumAttributeResource>();
            // Order
            CreateMap<Order, MinimumOrderResource>();
        }
    }
}