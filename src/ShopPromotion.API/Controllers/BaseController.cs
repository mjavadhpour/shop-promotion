// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers
{
    // API
    using ServiceConfiguration;
    using Helper.Infrastructure.Filters;
    // Domain
    using Domain.Services.PaginationHelper;
    using Domain.Infrastructure.DAL;

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
        /// Unit of work with all registered services.
        /// </summary>
        protected readonly UnitOfWork UnitOfWork;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected BaseController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork)
        {
            DefaultPagingOptions = defaultPagingOptionsAccessor;
            UnitOfWork = unitOfWork;
        }
    }
}