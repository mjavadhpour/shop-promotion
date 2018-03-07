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
    using App;
    using ServiceConfiguration;
    using Infrastructure.Models.Parameter;
    // Domain
    using Domain.Services.PaginationHelper;
    using Domain.EntityLayer;
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Form;
    using Domain.Infrastructure.Models.Form.Custom;
    using Domain.Infrastructure.Models.Resource;

    /// <summary>
    /// Shop status controller.
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class ShopStatusController : BaseController
    {
        private readonly ResolvedPaginationValueService _defaultPagingOptionsAccessor;
        private readonly UserManager<BaseIdentityUser> _userManager;
        private readonly UnitOfWork<ShopForm, MinimumShopResource, Shop> _genericUnitOfWork;

        /// <inheritdoc />
        public ShopStatusController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork, ResolvedPaginationValueService defaultPagingOptionsAccessor1,
            UserManager<BaseIdentityUser> userManager,
            UnitOfWork<ShopForm, MinimumShopResource, Shop> genericUnitOfWork) : base(defaultPagingOptionsAccessor,
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
        public async Task<IActionResult> UpdateShopStatusAsync([FromForm] ShopStatusForm shopStatusForm,
            CancellationToken ct)
        {
            var entity = new ShopController(_defaultPagingOptionsAccessor, UnitOfWork, _userManager, _genericUnitOfWork)
                .GetEntityByIdAsync(new GetItemByIdParameters {ItemId = shopStatusForm.ShopId}, ct).Result;
            if (entity.GetType() != typeof(OkObjectResult)) return NotFound();
            await UnitOfWork.ShopStatusService.ChangeShopStatus(shopStatusForm);
            return NoContent();
        }
    }
}