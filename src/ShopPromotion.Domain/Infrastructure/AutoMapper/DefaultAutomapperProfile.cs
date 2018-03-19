// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using AutoMapper;
using ShopPromotion.Domain.Infrastructure.Models.Form;

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
            // ShopPromotionReview
            CreateMap<ShopPromotionReviewForm, MinimumShopPromotionReviewResource>();
            CreateMap<ShopPromotionReviewForm, ShopPromotionReview>()
                .ForMember(dto => dto.AuthorId, opt => opt.MapFrom(form => form.CreatedById));
            // ShopPromotionLike
            CreateMap<ShopPromotionLikeForm, MinimumShopPromotionLikeResource>();
            CreateMap<ShopPromotionLikeForm, ShopPromotionLike>()
                .ForMember(dto => dto.LikedById, opt => opt.MapFrom(form => form.CreatedById));
            // ShopPromotionBarcode
            CreateMap<ShopPromotionBarcodeForm, ShopPromotionBarcode>();
            // OrderDiscountCoupon
            CreateMap<OrderDiscountCouponForm, OrderDiscountCoupon>();
            CreateMap<OrderDiscountCouponForm, MinimumOrderDiscountCouponResource>();
            // OrderItem
            CreateMap<OrderItemForm, OrderItem>();
            CreateMap<OrderItemForm, MinimumOrderItemResource>();
            // Discount
            CreateMap<DiscountCreateForm, Discount>();
            CreateMap<DiscountCreateForm, MinimumDiscountResource>();
            // ShopCheckoutRequest
            CreateMap<ShopCheckoutRequestForm, ShopCheckoutRequest>()
                .ForMember(dto => dto.ShopKeeperUserId, opt => opt.MapFrom(s => s.CreatedById));
            CreateMap<ShopCheckoutRequestForm, MinimumShopCheckoutRequestResource>();
            // PaymentMethod
            CreateMap<PaymentMethodForm, PaymentMethod>();
            // ShopImage
            CreateMap<ShopImage, MinimumShopImageResource>();
            // ShopGeolocations
            CreateMap<ShopGeolocation, MinimumShopGeolocationResource>();
            CreateMap<ShopGeolocationForm, ShopGeolocation>();
            CreateMap<ShopGeolocationForm, MinimumShopGeolocationResource>();
            // Message
            CreateMap<Message, MinimumMessageResource>();
            CreateMap<Message, MinimumMessageListResource>();
            CreateMap<MessageForm, Message>()
                .ForMember(m => m.AuthorId, opt => opt.MapFrom(mf => mf.CreatedById));
            // ShopKeeperUserInbox
            CreateMap<ShopKeeperUserInbox, MinimumInboxMessageResource>()
                .ForMember(dto => dto.Note, opt => opt.MapFrom(s => s.Message.Note))
                .ForMember(dto => dto.Subject, opt => opt.MapFrom(s => s.Message.Subject));
            CreateMap<ShopKeeperUserInbox,  MinimumInboxResource>()
                .ForMember(dto => dto.Subject, opt => opt.MapFrom(s => s.Message.Subject));
            // AppUserInbox
            CreateMap<AppUserInbox, MinimumInboxMessageResource>()
                .ForMember(dto => dto.Note, opt => opt.MapFrom(s => s.Message.Note))
                .ForMember(dto => dto.Subject, opt => opt.MapFrom(s => s.Message.Subject));
            CreateMap<AppUserInbox, MinimumInboxResource>()
                .ForMember(dto => dto.Subject, opt => opt.MapFrom(s => s.Message.Subject));
            // ShopInbox
            CreateMap<ShopInbox, MinimumInboxMessageResource>()
                .ForMember(dto => dto.Note, opt => opt.MapFrom(s => s.Message.Note))
                .ForMember(dto => dto.Subject, opt => opt.MapFrom(s => s.Message.Subject));
            CreateMap<ShopInbox, MinimumInboxResource>()
                .ForMember(dto => dto.Subject, opt => opt.MapFrom(s => s.Message.Subject));
            CreateMap<MessageForm, MinimumMessageResource>();
            // SpecialOffer
            CreateMap<SpecialOffer, MinimumSpecialOfferResource>();
            // Attribute
            CreateMap<Attribute, MinimumAttributeResource>();
            // Order
            CreateMap<Order, MinimumOrderResource>();
            CreateMap<OrderForm, Order>();
        }
    }
}