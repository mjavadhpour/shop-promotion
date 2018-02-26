// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Helper.Infrastructure.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// </summary>
    public class ShopPromotionHeadersMiddleware
    {
        /// <summary>
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// </summary>
        private readonly HeadersPolicy _policy;

        /// <summary>
        /// </summary>
        /// <param name="next"></param>
        /// <param name="policy"></param>
        public ShopPromotionHeadersMiddleware(RequestDelegate next, HeadersPolicy policy)
        {
            _policy = policy;
            _next = next;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Response.Headers;

            foreach (var headerValuePair in _policy.SetHeaders)
                headers[headerValuePair.Key] = headerValuePair.Value;

            foreach (var header in _policy.RemoveHeaders)
                headers.Remove(header);

            await _next.Invoke(context);
        }
    }
}