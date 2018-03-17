// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace ShopPromotion.Helper.Infrastructure.Filters
{
    using ActionResults;
    using Domain.Exceptions;

    /// <summary>
    /// There are a couple of ways to implement exception handling globally.
    /// </summary>
    public class ApiExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private readonly List<Func<Exception, IActionResult>> _handlers = new List<Func<Exception, IActionResult>>();

        private void AddHandler<T>(Func<T, IActionResult> handler) where T : Exception
        {
            _handlers.Add(ex => ex is T typed ? handler(typed) : null);
        }

        public ApiExceptionFilter()
        {
            AddHandler<ShopNotFoundException>(OnShopNotFoundException);
            AddHandler<PaymentMethodNotFoundException>(OnPaymentMethodNotFoundException);
            AddHandler<ShopPromotionNotFoundException>(OnShopPromotionNotFoundException);
            AddHandler<KeyNotFoundException>(OnKeyNotFoundException);
            AddHandler<GatewayConfigNotFoundException>(OnGatewayConfigNotFoundException);
            AddHandler<DuplicateShopGeolocationException>(OnDuplicateShopGeolocationException);
            AddHandler<NotValidBase64ImageException>(OnNotValidBase64ImageException);
            AddHandler<Exception>(OnOtherException);
        }

        private IActionResult OnShopNotFoundException(ShopNotFoundException ex)
        {
            return ErrorResult(404, new ApiError { Message = "Requested shop was not found!" });
        }

        private IActionResult OnPaymentMethodNotFoundException(PaymentMethodNotFoundException ex)
        {
            return ErrorResult(404, new ApiError { Message = "Requested payment method was not found!" });
        }

        private IActionResult OnKeyNotFoundException(KeyNotFoundException ex)
        {
            return ErrorResult(500, new ApiError { Message = "Requested query was not found in action parameters!" });
        }

        private IActionResult OnGatewayConfigNotFoundException(GatewayConfigNotFoundException ex)
        {
            return ErrorResult(404, new ApiError { Message = "Requested gateway config was not found!" });
        }

        private IActionResult OnShopPromotionNotFoundException(ShopPromotionNotFoundException ex)
        {
            return ErrorResult(404, new ApiError { Message = "Requested shop promotion was not found!" });
        }

        private IActionResult OnNotValidBase64ImageException(NotValidBase64ImageException ex)
        {
            return ErrorResult(400, new ApiError { Message = "Get an invalid image with base 64 format!" });
        }

        private IActionResult OnDuplicateShopGeolocationException(DuplicateShopGeolocationException ex)
        {
            return ErrorResult(400, new ApiError { Message = "Each shop can have just one location!" });
        }

        private IActionResult OnOtherException(Exception ex)
        {
            return ErrorResult(500, new ApiError { Message = "Something wrong. Internal error occured!" });
        }

        private IActionResult ErrorResult(int statusCode, ApiError error)
        {
            error.StatusCode = statusCode;
            return new ObjectResult(error) { StatusCode = statusCode };
        }

        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            IActionResult result = null;

            foreach (var handler in _handlers)
            {
                result = handler(context.Exception);

                if (result != null)
                {
                    break;
                }
            }

            if (result != null)
            {
                context.Result = result;
            }
        }
    }
}