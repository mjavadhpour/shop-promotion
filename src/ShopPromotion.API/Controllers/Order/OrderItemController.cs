// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.Order
{
    // API
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Form;
    // Domain
    using Domain.EntityLayer;
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response;
    using Domain.Infrastructure.Models.Response.Pagination;
    using Domain.Services.PaginationHelper;

    /// <summary>
    /// Shop controller.
    /// </summary>
    [Area("Order")]
    public class OrderItemController : BaseApiController<OrderItemForm, MinimumOrderItemListResource,
        MinimumOrderItemResource, OrderItem, GetAllOrderItemsParameters,
        GetItemByIdParameters>
    {
        private readonly int? _currentOrderId;

        /// <inheritdoc />
        public OrderItemController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork,
            UserManager<BaseIdentityUser> userManager,
            UnitOfWork<OrderItemForm, MinimumOrderItemListResource, MinimumOrderItemResource, OrderItem>
                genericUnitOfWork) : base(defaultPagingOptionsAccessor,
            unitOfWork, userManager, genericUnitOfWork)
        {
            var order = UnitOfWork.Context.Orders.Select(o => new {o.Id, o.State})
                .SingleOrDefault(o => o.State == OrderStateOptions.Cart);
            _currentOrderId = order?.Id;
        }

        /// <summary>
        /// Get list of orderItems for specific order.
        /// </summary>
        /// <param name="ct">
        /// Adding a CancellationToken parameter to your route methods allows ASP.NET Core to notify your
        /// asynchronous tasks of a cancellation (if the browser closes a connection, for example).
        /// </param>
        /// <param name="pagingOptions"></param>
        /// <param name="getAllOrderItemsParameters"></param>
        /// <returns>
        /// IActionResult gives you the flexibility to return both HTTP status codes and object payloads.
        /// return type contain a <see cref="T:Microsoft.AspNetCore.Mvc.IActionResult" />.
        /// </returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(Page<MinimumOrderItemListResource>), 200)]
        public override async Task<IActionResult> GetEntitiesAsync([FromQuery] PagingOptions pagingOptions,
            GetAllOrderItemsParameters getAllOrderItemsParameters, CancellationToken ct)
        {
            return await base.GetEntitiesAsync(pagingOptions, getAllOrderItemsParameters, ct);
        }

        public override async Task<IActionResult> GetEntityByIdAsync(GetItemByIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// <summary>
        /// Assign new order item to current order.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="ct">
        /// Adding a CancellationToken parameter to your route methods allows ASP.NET Core to notify your
        /// asynchronous tasks of a cancellation (if the browser closes a connection, for example).
        /// </param>
        /// <returns>
        /// IActionResult gives you the flexibility to return both HTTP status codes and object payloads.
        /// return type contain a <see cref="T:Microsoft.AspNetCore.Mvc.IActionResult" />.
        /// </returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(SingleModelResponse<MinimumOrderItemResource>), 201)]
        public override async Task<IActionResult> CreateEntityAsync([FromBody] OrderItemForm form, CancellationToken ct)
        {
            form.OrderId = _currentOrderId ?? 0;
            return await base.CreateEntityAsync(form, ct);
        }

        [NonAction]
        public override async Task<IActionResult> UpdateEntityAsync(GetItemByIdParameters itemByIdParameters,
            [FromBody] OrderItemForm form,
            CancellationToken ct)
        {
            return await base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// <summary>
        /// Delete existing order item from current order.
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="ct">
        /// Adding a CancellationToken parameter to your route methods allows ASP.NET Core to notify your
        /// asynchronous tasks of a cancellation (if the browser closes a connection, for example).
        /// </param>
        /// <returns>
        /// IActionResult gives you the flexibility to return both HTTP status codes and object payloads.
        /// return type contain a <see cref="T:Microsoft.AspNetCore.Mvc.IActionResult" />.
        /// </returns>
        /// <response code="204">Deleted</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        public override async Task<IActionResult> DeleteEntityAsync(GetItemByIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}