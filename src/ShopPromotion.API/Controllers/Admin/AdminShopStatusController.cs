// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.Admin
{
    // API
    using ServiceConfiguration;
    // Domain
    using Domain.Services.PaginationHelper;
    using Domain.EntityLayer;
    using Domain.Infrastructure.DAL;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Form.Custom;
    using Domain.Infrastructure.Models.Resource;

    /// <summary>
    /// Shop status controller.
    /// </summary>
    [Area("Admin")]
    [Route("api/v1/[area]/ShopStatus")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class AdminShopStatusController : BaseController
    {
        private readonly ResolvedPaginationValueService _defaultPagingOptionsAccessor;
        private readonly UserManager<BaseIdentityUser> _userManager;
        private readonly UnitOfWork<ShopForm, MinimumShopListResource, MinimumShopResource, Shop> _genericUnitOfWork;

        /// <inheritdoc />
        public AdminShopStatusController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork, ResolvedPaginationValueService defaultPagingOptionsAccessor1,
            UserManager<BaseIdentityUser> userManager,
            UnitOfWork<ShopForm, MinimumShopListResource, MinimumShopResource, Shop> genericUnitOfWork) : base(defaultPagingOptionsAccessor,
            unitOfWork)
        {
            _defaultPagingOptionsAccessor = defaultPagingOptionsAccessor1;
            _userManager = userManager;
            _genericUnitOfWork = genericUnitOfWork;
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
        public async Task<IActionResult> UpdateShopStatusAsync([FromBody] ShopStatusForm shopStatusForm,
            CancellationToken ct)
        {
            await UnitOfWork.ShopStatusService.ChangeShopStatus(shopStatusForm, ct);
            await UnitOfWork.SaveAsync();
            return NoContent();
        }
    }
}