// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Root controller.
    /// </summary>
    [Route("/")]
    public class RootController : Controller
    {
        /// <summary>
        /// Root action.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            return new RedirectResult("~/swagger");
        }
    }
}