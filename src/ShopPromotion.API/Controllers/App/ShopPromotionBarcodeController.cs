// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopPromotion.API.ServiceConfiguration;

namespace ShopPromotion.API.Controllers.App
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
    /// Shop promotion barcode controller.
    /// </summary>
    [Area("App")]
    [Route("api/v1/[area]/Shop/{ShopId}/Promotion")]
    [Authorize(Policy = ConfigurePolicyService.ShopKeeperUserPolicy)]
    public class ShopPromotionBarcodeController : BaseApiController<ShopPromotionBarcodeForm,
        MinimumShopPromotionBarcodeResource, MinimumShopPromotionBarcodeResource, ShopPromotionBarcode,
        GetAllShopPromotionBarcodeParameters, GetItemByIdAndShopPromotionIdParameters>
    {
        public ShopPromotionBarcodeController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork, UserManager<BaseIdentityUser> userManager,
            UnitOfWork<ShopPromotionBarcodeForm, MinimumShopPromotionBarcodeResource,
                MinimumShopPromotionBarcodeResource, ShopPromotionBarcode> genericUnitOfWork, IMediator mediator) :
            base(defaultPagingOptionsAccessor, unitOfWork, userManager, genericUnitOfWork, mediator)
        {
        }

        /// <summary>
        /// Get list of shop promotion barcodes.
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
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{PromotionId}/Barcode")]
        [ProducesResponseType(typeof(Page<MinimumShopPromotionBarcodeResource>), 200)]
        public override async Task<IActionResult> GetEntitiesAsync([FromQuery] PagingOptions pagingOptions,
            GetAllShopPromotionBarcodeParameters getAllShopsParameters, CancellationToken ct)
        {
            return await base.GetEntitiesAsync(pagingOptions, getAllShopsParameters, ct);
        }

        /// <summary>
        /// Get a shop promotion barcode by id.
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
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{ShopPromotionId}/Barcode/{ItemId}")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumShopPromotionBarcodeResource>), 200)]
        public override async Task<IActionResult> GetEntityByIdAsync(
            GetItemByIdAndShopPromotionIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// <summary>
        /// Create new shop promotion barcode.
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
        [HttpPost("{ShopPromotionId}/Barcode")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumShopPromotionBarcodeResource>), 201)]
        public override async Task<IActionResult> CreateEntityAsync(
            [FromBody] ShopPromotionBarcodeForm form,
            CancellationToken ct)
        {
            // Manually assign route value to form.
            form.PromotionId = Int32.Parse(HttpContext.GetRouteValue("ShopPromotionId").ToString());
            return await base.CreateEntityAsync(form, ct);
        }

        /// <summary>
        /// Update existing shop promotion barcode.
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="form"></param>
        /// <param name="ct">
        /// Adding a CancellationToken parameter to your route methods allows ASP.NET Core to notify your
        /// asynchronous tasks of a cancellation (if the browser closes a connection, for example).
        /// </param>
        /// <returns>
        /// IActionResult gives you the flexibility to return both HTTP status codes and object payloads.
        /// return type contain a <see cref="T:Microsoft.AspNetCore.Mvc.IActionResult" />.
        /// </returns>
        /// <response code="204">Updated</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{ShopPromotionId}/Barcode/{ItemId}")]
        public override async Task<IActionResult> UpdateEntityAsync(
            GetItemByIdAndShopPromotionIdParameters itemByIdParameters,
            [FromBody] ShopPromotionBarcodeForm form,
            CancellationToken ct)
        {
            // Manually assign route value to form.
            form.PromotionId = itemByIdParameters.ShopPromotionId;
            return await base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// <summary>
        /// Delete existing shop promotion barcode.
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
        [HttpDelete("{ShopPromotionId}/Barcode/{ItemId}")]
        public override async Task<IActionResult> DeleteEntityAsync(
            GetItemByIdAndShopPromotionIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}