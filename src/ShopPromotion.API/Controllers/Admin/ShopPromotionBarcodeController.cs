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
    /// Shop promotion controller.
    /// </summary>
    [Area("Admin")]
    [Route("api/v1/[area]")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class ShopPromotionBarcodeController : BaseApiController<ShopPromotionBarcodeForm, MinimumShopPromotionBarcodeResource, MinimumShopPromotionBarcodeResource, ShopPromotionBarcode
        , GetAllShopPromotionBarcodeParameters, GetItemByIdAndShopAndPromotionParameters>
    {
        /// <inheritdoc />
        public ShopPromotionBarcodeController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork, UserManager<BaseIdentityUser> userManager,
            UnitOfWork<ShopPromotionBarcodeForm, MinimumShopPromotionBarcodeResource, MinimumShopPromotionBarcodeResource, ShopPromotionBarcode> genericUnitOfWork) : base(
            defaultPagingOptionsAccessor, unitOfWork, userManager, genericUnitOfWork)
        {
        }

        /// Get list of shop promotion barcodes.
        /// <summary>
        /// Get list of shop promotion barcodes.
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
        [HttpGet("Shop/{ShopId}/Promotion/{PromotionId}/[controller]")]
        [ProducesResponseType(typeof(Page<MinimumShopPromotionBarcodeResource>), 200)]
        public override Task<IActionResult> GetEntitiesAsync(PagingOptions pagingOptions,
            GetAllShopPromotionBarcodeParameters entityTypeParameters, CancellationToken ct)
        {
            return base.GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override Task<IActionResult> GetEntityByIdAsync(GetItemByIdAndShopAndPromotionParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override Task<IActionResult> CreateEntityAsync([FromBody] ShopPromotionBarcodeForm form, CancellationToken ct)
        {
            return base.CreateEntityAsync(form, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override Task<IActionResult> UpdateEntityAsync(GetItemByIdAndShopAndPromotionParameters itemByIdParameters,
            [FromBody] ShopPromotionBarcodeForm form, CancellationToken ct)
        {
            return base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override Task<IActionResult> DeleteEntityAsync(GetItemByIdAndShopAndPromotionParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}