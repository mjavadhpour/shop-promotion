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
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Form;
    using ServiceConfiguration;
    // Domain
    using Domain.EntityLayer;
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response.Pagination;
    using Domain.Services.PaginationHelper;

    /// <summary>
    /// Shop controller.
    /// </summary>
    [Area("Admin")]
    [Route("api/v1/[area]/Shop")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class AdminShopController : BaseApiController<ShopAdminForm, MinimumShopListResource, MinimumShopResource, Shop, GetAllShopsParameters,
        GetItemByIdParameters>
    {
        /// <inheritdoc />
        public AdminShopController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork,
            UserManager<BaseIdentityUser> userManager,
            UnitOfWork<ShopAdminForm, MinimumShopListResource, MinimumShopResource, Shop> genericUnitOfWork) : base(defaultPagingOptionsAccessor,
            unitOfWork, userManager, genericUnitOfWork)
        {
        }

        /// Get list of shops.
        /// <summary>
        /// Get list of shops.
        /// </summary>
        /// <param name="ct">
        /// Adding a CancellationToken parameter to your route methods allows ASP.NET Core to notify your
        /// asynchronous tasks of a cancellation (if the browser closes a connection, for example).
        /// </param>
        /// <param name="pagingOptions"></param>
        /// <param name="getAllShopsParameters"></param>
        /// <returns>
        /// IActionResult gives you the flexibility to return both HTTP status codes and object payloads.
        /// return type contain a <see cref="T:Microsoft.AspNetCore.Mvc.IActionResult" />.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(Page<MinimumShopResource>), 200)]
        public override async Task<IActionResult> GetEntitiesAsync([FromQuery] PagingOptions pagingOptions,
            GetAllShopsParameters getAllShopsParameters, CancellationToken ct)
        {
            return await base.GetEntitiesAsync(pagingOptions, getAllShopsParameters, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override async Task<IActionResult> GetEntityByIdAsync(GetItemByIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override async Task<IActionResult> CreateEntityAsync([FromBody] ShopAdminForm form, CancellationToken ct)
        {
            return await base.CreateEntityAsync(form, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override async Task<IActionResult> UpdateEntityAsync(GetItemByIdParameters itemByIdParameters,
            [FromBody] ShopAdminForm form,
            CancellationToken ct)
        {
            return await base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override async Task<IActionResult> DeleteEntityAsync(GetItemByIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}