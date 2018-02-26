// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Helper.Infrastructure.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Base ShopPromotion middleware class.
    /// </summary>
    public class ShopPromotionOptionsMiddleware
    {
        /// <summary>
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// </summary>
        /// <param name="next"></param>
        public ShopPromotionOptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            BeginInvokeForOptionMethod(context);
            // do not call _next.Invoke if method is options, 
            // the request should end after calling context.Response.WriteAsync("OK");
            if (context.Request.Method != "OPTIONS") await _next.Invoke(context);
        }

        /// <summary>
        /// Return response 200 status code with all allowed origin and
        /// necessary headers for http OPTION method.
        /// </summary>
        /// <param name="context"></param>
        private static async void BeginInvokeForOptionMethod(HttpContext context)
        {
            if (context.Request.Method != "OPTIONS") return;

            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Headers",
                new[] {"Origin, X-Requested-With, Content-Type, Accept, Authorization"});
            context.Response.Headers.Add("Access-Control-Allow-Methods", new[] {"GET, POST, PUT, DELETE, OPTIONS"});
            context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] {"true"});
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("");
        }
    }
}