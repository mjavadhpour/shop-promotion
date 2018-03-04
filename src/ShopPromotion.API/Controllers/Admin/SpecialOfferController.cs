// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopPromotion.API.Infrastructure.Models.Parameter;
using ShopPromotion.API.ServiceConfiguration;
using ShopPromotion.Domain.EntityLayer;
using ShopPromotion.Domain.ModelLayer.Form;
using ShopPromotion.Domain.ModelLayer.Parameter;
using ShopPromotion.Domain.ModelLayer.Resource;
using ShopPromotion.Domain.ModelLayer.Response;
using ShopPromotion.Domain.ModelLayer.Response.Pagination;
using ShopPromotion.Domain.Services;
using ShopPromotion.Domain.Services.PaginationHelper;

namespace ShopPromotion.API.Controllers.Admin
{
    /// <summary>
    /// Shop special offer controller.
    /// </summary>
    [Area("Admin")]
    [Route("api/v1/[area]")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class SpecialOfferController : BaseApiController<SpeicialOfferForm, MinimumSpecialOfferResource, SpecialOffer
        , GetAllSpecialOffersParameters, GetItemByIdAndShopParameters>
    {
        /// <inheritdoc />
        public SpecialOfferController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            IBaseService<SpeicialOfferForm, MinimumSpecialOfferResource, SpecialOffer> entityService,
            UserManager<BaseIdentityUser> userManager) : base(defaultPagingOptionsAccessor, entityService, userManager)
        {
        }

        /// Get list of shop special offers special offers.
        /// <summary>
        /// Get list of shop special offers special offers.
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
        [HttpGet("[controller]")]
        [ProducesResponseType(typeof(Page<MinimumSpecialOfferResource>), 200)]
        public override Task<IActionResult> GetEntitiesAsync(PagingOptions pagingOptions,
            GetAllSpecialOffersParameters entityTypeParameters, CancellationToken ct)
        {
            return base.GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);
        }

        /// Get a shop special offer by id.
        /// <summary>
        /// Get a shop special offer by id.
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
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("shop/{shopId}/[controller]/{itemId}")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumSpecialOfferResource>), 200)]
        public override Task<IActionResult> GetEntityByIdAsync(GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// Create new shop special offer.
        /// <summary>
        /// Create new shop special offer.
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
        [HttpPost("shop/{shopId}/[controller]")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumSpecialOfferResource>), 201)]
        public override Task<IActionResult> CreateEntityAsync(SpeicialOfferForm form, CancellationToken ct)
        {
            return base.CreateEntityAsync(form, ct);
        }

        /// Update existing shop special offer.
        /// <summary>
        /// Update existing shop special offer.
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
        [HttpPut("shop/{shopId}/[controller]/{itemId}")]
        public override Task<IActionResult> UpdateEntityAsync(GetItemByIdAndShopParameters itemByIdParameters,
            SpeicialOfferForm form, CancellationToken ct)
        {
            return base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// Delete existing shop special offer.
        /// <summary>
        /// Delete existing shop special offer.
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
        [HttpDelete("shop/{shopId}/[controller]/{itemId}")]
        public override Task<IActionResult> DeleteEntityAsync(GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}