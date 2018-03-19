// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
    /// Shop promotion controller.
    /// </summary>
    [Area("Admin")]
    [Route("api/v1/[area]/Order")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class AdminOrderController : BaseApiController<OrderForm, MinimumOrderResource, MinimumOrderResource, Order
        , GetAllAdminOrdersParameters, GetItemByIdAndShopParameters>
    {
        public AdminOrderController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork,
            UserManager<BaseIdentityUser> userManager,
            UnitOfWork<OrderForm, MinimumOrderResource, MinimumOrderResource, Order> genericUnitOfWork,
            IMediator mediator) : base(defaultPagingOptionsAccessor, unitOfWork, userManager, genericUnitOfWork,
            mediator)
        {
        }

        /// <summary>
        /// Get list of orders.
        /// </summary>
        /// <param name="ct">
        /// Adding a CancellationToken parameter to your route methods allows ASP.NET Core to notify your
        /// asynchronous tasks of a cancellation (if the browser closes a connection, for example).
        /// </param>
        /// <param name="pagingOptions"></param>
        /// <param name="entityTypeParameters"></param>
        /// <returns>
        /// IActionResult gives you the flexibility to return both HTTP status codes and object payloads.
        /// return type contain a <see cref="T:Microsoft.AspNetCore.Mvc.IActionResult" />.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("")]
        [ProducesResponseType(typeof(Page<MinimumOrderResource>), 200)]
        public override Task<IActionResult> GetEntitiesAsync(PagingOptions pagingOptions,
            GetAllAdminOrdersParameters entityTypeParameters, CancellationToken ct)
        {
            return base.GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override Task<IActionResult> GetEntityByIdAsync(GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override Task<IActionResult> CreateEntityAsync([FromBody] OrderForm form, CancellationToken ct)
        {
            return base.CreateEntityAsync(form, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override Task<IActionResult> UpdateEntityAsync(GetItemByIdAndShopParameters itemByIdParameters,
            [FromBody] OrderForm form, CancellationToken ct)
        {
            return base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override Task<IActionResult> DeleteEntityAsync(GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}