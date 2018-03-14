﻿// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using ShopPromotion.Domain.Exceptions;

namespace ShopPromotion.Helper.Infrastructure.Filters
{
    using ActionResults;

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