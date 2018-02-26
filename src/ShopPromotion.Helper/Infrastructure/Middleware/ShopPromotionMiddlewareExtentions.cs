// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Helper.Infrastructure.Middleware
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// The current standard approach to registering middleware in Startup is to create an extension method on
    /// IApplicationBuilder that registers the correct class.
    /// </summary>
    /// <remarks>
    /// Note that it is no longer considered best practice to create an extension method which takes a delegate.
    /// https://blogs.msdn.microsoft.com/webdev/2016/04/28/notes-from-the-asp-net-community-standup-april-26-2016
    /// </remarks>
    public static class ShopPromotionMiddlewareExtentions
    {
        /// <summary>
        /// Return configure an application's request pipeline for handle option method.
        /// </summary>
        /// <param name="applicationBuilder">
        /// The <see cref="T:Microsoft.AspNetCore.Builder.IApplicationBuilder" /> instance.
        /// </param>
        /// <returns> The <see cref="T:Microsoft.AspNetCore.Builder.IApplicationBuilder" /> instance.</returns>
        public static IApplicationBuilder UseShopPromotionOptionsMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ShopPromotionOptionsMiddleware>();
        }

        /// <summary>
        /// Return configure an application's for add intended headers to request pipeline.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="shopPromotionHeadersBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseShopPromotionHeadersMiddleware(this IApplicationBuilder applicationBuilder,
            ShopPromotionHeadersBuilder shopPromotionHeadersBuilder)
        {
            var policy = shopPromotionHeadersBuilder.Build();
            return applicationBuilder.UseMiddleware<ShopPromotionHeadersMiddleware>(policy);
        }
    }
}