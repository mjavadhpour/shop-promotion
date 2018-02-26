// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ShopPromotion.API.Controllers
{
    using Domain.ModelLayer.Response.Pagination;
    using Helper.Infrastructure.Filters;

    /// <summary>
    /// Base Controller that SOULD extended from all APIs.
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ValidateModel]
    [ApiExceptionFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// Pagination settings.
        /// </summary>
        protected readonly IPagingOptions DefaultPagingOptions;

        /// <summary>
        /// Cunstructor.
        /// </summary>
        protected BaseController(IOptions<PagingOptions> defaultPagingOptionsAccessor)
        {
            DefaultPagingOptions = defaultPagingOptionsAccessor.Value;
        }
    }
}