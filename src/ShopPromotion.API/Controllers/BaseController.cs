// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopPromotion.Domain.Services.PaginationHelper;

namespace ShopPromotion.API.Controllers
{
    using ServiceConfiguration;
    using Helper.Infrastructure.Filters;

    /// <summary>
    /// Base Controller that SOULD extended from all APIs.
    /// </summary>
    [ApiVersion( "1.0" )]
    [Produces("application/json")]
    [Route("api/v1/[area]/[controller]")]
    [Authorize(Policy = ConfigurePolicyService.AppUserPolicy)]
    [ValidateModel]
    [ApiExceptionFilter]
    [ServiceFilter(typeof(PaginationDefaultValueFilter))]
    public class BaseController : Controller
    {
        /// <summary>
        /// Pagination settings.
        /// </summary>
        protected readonly ResolvedPaginationValueService DefaultPagingOptions;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseController(ResolvedPaginationValueService defaultPagingOptionsAccessor)
        {
            DefaultPagingOptions = defaultPagingOptionsAccessor;
        }
    }
}