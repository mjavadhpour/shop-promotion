// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
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
    /// Shop promotion review controller.
    /// </summary>
    [Area("App")]
    [Route("api/v1/[area]/shopPromotion")]
    public class ShopPromotionLikeController : BaseApiController<ShopPromotionLikeForm,
        MinimumShopPromotionLikeListResource, MinimumShopPromotionLikeResource, ShopPromotionLike,
        GetAllShopPromotionLikesParameters, GetItemByIdAndShopPromotionIdParameters>
    {
        /// <inheritdoc />
        public ShopPromotionLikeController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork,
            UserManager<BaseIdentityUser> userManager,
            UnitOfWork<ShopPromotionLikeForm, MinimumShopPromotionLikeListResource,
                MinimumShopPromotionLikeResource, ShopPromotionLike> genericUnitOfWork) : base(
            defaultPagingOptionsAccessor,
            unitOfWork, userManager, genericUnitOfWork)
        {
        }

        /// <summary>
        /// Get list of shop promotion reviews.
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
        [HttpGet("{ShopPromotionId}/Like")]
        [ProducesResponseType(typeof(Page<MinimumShopPromotionLikeListResource>), 200)]
        [Authorize(Policy = ConfigurePolicyService.ShopKeeperUserPolicy)]
        public override async Task<IActionResult> GetEntitiesAsync([FromQuery] PagingOptions pagingOptions,
            GetAllShopPromotionLikesParameters getAllShopsParameters, CancellationToken ct)
        {
            return await base.GetEntitiesAsync(pagingOptions, getAllShopsParameters, ct);
        }

        /// <summary>
        /// Get a shop promotion like by id.
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
        [HttpGet("{ShopPromotionId}/Like/{ItemId}")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumShopPromotionLikeResource>), 200)]
        public override async Task<IActionResult> GetEntityByIdAsync(
            GetItemByIdAndShopPromotionIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// <summary>
        /// Like or ulike promotion.
        /// </summary>
        /// <remarks>
        /// If user previously liked the target, we dislike and if not liked before, we like the target.
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
        /// <response code="201">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("{ShopPromotionId}/Like")]
        public override async Task<IActionResult> CreateEntityAsync(
            [FromBody] ShopPromotionLikeForm form,
            CancellationToken ct)
        {
            var createdBy = UserManager.GetUserId(HttpContext.User);
            // Manually assign route value to form.
            form.ShopPromotionId = Int32.Parse(HttpContext.GetRouteValue("ShopPromotionId").ToString());
            var spLike = UnitOfWork.Context.ShopPromotionLikes.AsNoTracking()
                .Select(x => new {x.LikedById, x.ShopPromotionId, x.Liked, x.Id})
                .SingleOrDefaultAsync(
                x => x.LikedById == createdBy
                     && 
                     x.ShopPromotionId == form.ShopPromotionId
                , ct);

            if (spLike.Result != null)
            {
                form.UpdatedAt = DateTime.Now;
                form.CreatedById = createdBy;
                form.Liked = spLike.Result.Liked;
                return await base.UpdateEntityAsync(
                    new GetItemByIdAndShopPromotionIdParameters
                    {
                        ItemId = spLike.Result.Id,
                        ShopPromotionId = form.ShopPromotionId
                    }, form, ct);
            }

            return await base.CreateEntityAsync(form, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override async Task<IActionResult> UpdateEntityAsync(
            GetItemByIdAndShopPromotionIdParameters itemByIdParameters,
            [FromBody] ShopPromotionLikeForm form,
            CancellationToken ct)
        {
            // Manually assign route value to form.
            form.ShopPromotionId = itemByIdParameters.ShopPromotionId;
            return await base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// <inheritdoc />
        [NonAction]
        public override async Task<IActionResult> DeleteEntityAsync(
            GetItemByIdAndShopPromotionIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}