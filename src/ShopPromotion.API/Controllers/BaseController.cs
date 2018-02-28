// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ShopPromotion.API.Controllers
{
    using ServiceConfiguration;
    using Domain.ModelLayer.Response.Pagination;
    using Helper.Infrastructure.Filters;

    /// <summary>
    /// Base Controller that SOULD extended from all APIs.
    /// </summary>
    [ApiVersion( "1.0" )]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Policy = ConfigurePolicyService.AppUserPolicy)]
    [ValidateModel]
    [ApiExceptionFilter]
    public class BaseController : Controller
    {
        /// <summary>
        /// Pagination settings.
        /// </summary>
        protected readonly IPagingOptions DefaultPagingOptions;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseController(IOptions<PagingOptions> defaultPagingOptionsAccessor)
        {
            DefaultPagingOptions = defaultPagingOptionsAccessor.Value;
        }
    }
}