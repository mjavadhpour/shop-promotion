// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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
    using Domain.Infrastructure.Models.Response;
    using Domain.Infrastructure.Models.Response.Pagination;
    using Domain.Services.PaginationHelper;

    /// <summary>
    /// Shop promotion controller.
    /// </summary>
    [Area("Admin")]
    [Route("api/v1/[area]")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class AdminShopPromotionController : BaseApiController<ShopPromotionForm, MinimumShopPromotionResource, MinimumShopPromotionResource, ShopPromotion
        , GetAllShopPromotionParameters, GetItemByIdAndShopParameters>
    {
        /// <inheritdoc />
        public AdminShopPromotionController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork, UserManager<BaseIdentityUser> userManager,
            UnitOfWork<ShopPromotionForm, MinimumShopPromotionResource, MinimumShopPromotionResource, ShopPromotion> genericUnitOfWork) : base(
            defaultPagingOptionsAccessor, unitOfWork, userManager, genericUnitOfWork)
        {
        }

        /// <summary>
        /// Get list of shop promotions for Admin.
        /// </summary>
        /// <remarks>
        /// ### IMPORTANT NOTE:
        /// 
        /// Return promotions of <b>Approved</b> shops.
        /// </remarks>
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
        [HttpGet("ShopPromotion")]
        [ProducesResponseType(typeof(Page<MinimumShopPromotionResource>), 200)]
        public override Task<IActionResult> GetEntitiesAsync(PagingOptions pagingOptions,
            GetAllShopPromotionParameters entityTypeParameters, CancellationToken ct)
        {
            return base.GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);
        }

        /// <summary>
        /// Get a shop promotion for Admin by id.
        /// </summary>
        /// <remarks>
        /// ### IMPORTANT NOTE:
        /// 
        /// Return promotions of <b>Approved</b> shops.
        /// </remarks>
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
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("Shop/{ShopId}/ShopPromotion/{ItemId}")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumShopPromotionResource>), 200)]
        public override Task<IActionResult> GetEntityByIdAsync(GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// <summary>
        /// Create new shop promotion by Admin.
        /// </summary>
        /// <remarks>
        /// ### IMPORTANT NOTE:
        /// Return promotions of <b>Approved</b> shops.
        /// </remarks>
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
        [HttpPost("shop/{ShopId}/ShopPromotion")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumShopPromotionResource>), 201)]
        public override Task<IActionResult> CreateEntityAsync([FromBody] ShopPromotionForm form, CancellationToken ct)
        {
            form.ShopId = Int32.Parse(HttpContext.GetRouteValue("ShopId").ToString());
            return base.CreateEntityAsync(form, ct);
        }

        /// <summary>
        /// Update existing shop promotion by Admin.
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
        [HttpPut("Shop/{ShopId}/ShopPromotion/{ItemId}")]
        public override Task<IActionResult> UpdateEntityAsync(GetItemByIdAndShopParameters itemByIdParameters,
            ShopPromotionForm form, CancellationToken ct)
        {
            return base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// <summary>
        /// Delete existing shop promotion by Admin.
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
        [HttpDelete("shop/{ShopId}/ShopPromotion/{ItemId}")]
        public override Task<IActionResult> DeleteEntityAsync(GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}