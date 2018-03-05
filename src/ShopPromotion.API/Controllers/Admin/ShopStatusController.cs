// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.Admin
{
    // API
    using ServiceConfiguration;
    using Infrastructure.Models.Form;
    using Services.ShopStatus;
    // Domain
    using Domain.Services.PaginationHelper;

    /// <summary>
    /// Shop status controller.
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class ShopStatusController : BaseController
    {
        private readonly IShopStatusService _shopStatusService;

        /// <inheritdoc />
        protected ShopStatusController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            IShopStatusService shopStatusService) : base(defaultPagingOptionsAccessor)
        {
            _shopStatusService = shopStatusService;
        }

        /// <summary>
        /// Change shop status.
        /// </summary>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        public async Task<IActionResult> UpdateShopStatusAsync([FromForm] ShopStatusForm shopStatusForm)
        {
            await _shopStatusService.ChangeShopStatus(shopStatusForm);
            return NoContent();
        }
    }
}