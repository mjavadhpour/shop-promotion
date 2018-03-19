// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ShopPromotion.API.Controllers.App
{
    // API
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Form;
    // Domain
    using Domain.EntityLayer;
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response.Pagination;
    using Domain.Services.PaginationHelper;

    /// <summary>
    /// Shop geolocation controller.
    /// </summary>
    [Area("App")]
    [Route("api/v1/[area]/Shop/Location")]
    public class ShopGeolocationController : BaseApiController<ShopGeolocationForm,
        MinimumShopGeolocationListResource, MinimumShopGeolocationResource, ShopGeolocation,
        GetAllShopGeolocationsParameters, GetItemByIdAndShopParameters>
    {
        public ShopGeolocationController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork, UserManager<BaseIdentityUser> userManager,
            UnitOfWork<ShopGeolocationForm, MinimumShopGeolocationListResource, MinimumShopGeolocationResource,
                ShopGeolocation> genericUnitOfWork, IMediator mediator) : base(defaultPagingOptionsAccessor, unitOfWork,
            userManager, genericUnitOfWork, mediator)
        {
        }

        /// <summary>
        /// Get list of shop geolocations.
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
        [HttpGet("")]
        [ProducesResponseType(typeof(Page<MinimumShopGeolocationListResource>), 200)]
        public override async Task<IActionResult> GetEntitiesAsync([FromQuery] PagingOptions pagingOptions,
            GetAllShopGeolocationsParameters getAllShopsParameters, CancellationToken ct)
        {
            return await base.GetEntitiesAsync(pagingOptions, getAllShopsParameters, ct);
        }

        [NonAction]
        public override async Task<IActionResult> GetEntityByIdAsync(
            GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        [NonAction]
        public override async Task<IActionResult> CreateEntityAsync(
            [FromBody] ShopGeolocationForm form,
            CancellationToken ct)
        {
            // Manually assign route value to form.
            form.ShopId = Int32.Parse(HttpContext.GetRouteValue("ShopId").ToString());
            return await base.CreateEntityAsync(form, ct);
        }

        [NonAction]
        public override async Task<IActionResult> UpdateEntityAsync(
            GetItemByIdAndShopParameters itemByIdParameters,
            [FromBody] ShopGeolocationForm form,
            CancellationToken ct)
        {
            return await base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        [NonAction]
        public override async Task<IActionResult> DeleteEntityAsync(
            GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            return await base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}