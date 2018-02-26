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
            AddHandler<Exception>(OnOtherException);
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