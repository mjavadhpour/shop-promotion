// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopPromotion.Helper.Infrastructure.Filters
{
    using ActionResults;

    /// <summary>
    /// Validate model binding with an ActionFilter.
    /// </summary>
    /// <remarks> Please use [ValidateModel] as annotation in controller to triggered this method.</remarks>
    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(new ApiError(context.ModelState));
        }
    }
}