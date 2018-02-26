// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers
{
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
        /// Cunstructor.
        /// </summary>
        protected BaseController()
        {
        }
    }
}