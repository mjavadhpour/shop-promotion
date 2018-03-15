// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.App
{
    // API
    using Infrastructure.Models.Parameter;
    // Doamin
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response.Pagination;
    using Domain.Services.PaginationHelper;
    using Domain.Infrastructure.Models.Response;
    // Helper
    using Helper.Infrastructure.ActionResults;

    /// <summary>
    /// Message controller.
    /// </summary>
    [Area("App")]
    public class InboxController : BaseController
    {
        /// <inheritdoc />
        public InboxController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork)
            : base(defaultPagingOptionsAccessor, unitOfWork)
        {
        }

        /// <summary>
        /// Get list of messages related to requested shop. The shop inbox.
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
        /// <response code="500">Internal Server Error</response>
        [HttpGet("Shop/{ShopId}")]
        [ProducesResponseType(typeof(Page<MinimumInboxResource>), 200)]
        public async Task<IActionResult> GetShopInboxAsync(PagingOptions pagingOptions,
            GetAllShopInboxParameters entityTypeParameters, CancellationToken ct)
        {
            var entities = await UnitOfWork.InboxService
                .GetShopInboxMessagesAsync(entityTypeParameters, ct);

            var collection = Page<MinimumInboxResource>.Create(
                entities.Results.ToArray(),
                entities.TotalNumberOfRecords,
                DefaultPagingOptions);

            return Ok(collection);
        }

        /// <summary>
        /// Get requested message from shop inbox.
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="ct"></param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(SingleModelResponse<MinimumInboxMessageResource>), 200)]
        [HttpGet("Shop/{ShopId}/Message/{ItemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> GetShopMessageByIdAsync(GetItemByIdAndShopParameters itemByIdParameters,
            CancellationToken ct)
        {
            var response =
                new SingleModelResponse<MinimumInboxMessageResource>() as
                    ISingleModelResponse<MinimumInboxMessageResource>;
            // Unified model for single response.
            response.Model = await UnitOfWork.InboxService.GetShopInboxMessageByIdAsync(itemByIdParameters.ItemId, ct);
            if (response.Model == null) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Get list of messages related to requested user. The user inbox.
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
        /// <response code="500">Internal Server Error</response>
        [HttpGet("User/{PhoneNumber}")]
        [ProducesResponseType(typeof(Page<MinimumInboxResource>), 200)]
        public async Task<IActionResult> GetUserInboxAsync(PagingOptions pagingOptions,
            GetAllAppUserInboxParameters entityTypeParameters, CancellationToken ct)
        {
            var entities = await UnitOfWork.InboxService
                .GetAppUserInboxMessagesAsync(entityTypeParameters, ct);

            var collection = Page<MinimumInboxResource>.Create(
                entities.Results.ToArray(),
                entities.TotalNumberOfRecords,
                DefaultPagingOptions);

            return Ok(collection);
        }

        /// <summary>
        /// Get requested message from user inbox.
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="ct"></param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(SingleModelResponse<MinimumInboxMessageResource>), 200)]
        [HttpGet("User/{PhoneNumber}/Message/{ItemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> GetUserMessageByIdAsync(GetItemByIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            var response =
                new SingleModelResponse<MinimumInboxMessageResource>() as
                    ISingleModelResponse<MinimumInboxMessageResource>;
            // Unified model for single response.
            response.Model =
                await UnitOfWork.InboxService.GetAppUserInboxMessageByIdAsync(itemByIdParameters.ItemId, ct);
            if (response.Model == null) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Get list of messages related to requested user. The user inbox.
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
        /// <response code="500">Internal Server Error</response>
        [HttpGet("ShopKeeperUser/{PhoneNumber}")]
        [ProducesResponseType(typeof(Page<MinimumInboxResource>), 200)]
        public async Task<IActionResult> GetShopKeeperUserInboxAsync(PagingOptions pagingOptions,
            GetAllShopKeeperUserInboxParameters entityTypeParameters, CancellationToken ct)
        {
            var entities = await UnitOfWork.InboxService
                .GetShopKeeperUserInboxMessagesAsync(entityTypeParameters, ct);

            var collection = Page<MinimumInboxResource>.Create(
                entities.Results.ToArray(),
                entities.TotalNumberOfRecords,
                DefaultPagingOptions);

            return Ok(collection);
        }

        /// <summary>
        /// Get requested message from user inbox.
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="ct"></param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(SingleModelResponse<MinimumInboxMessageResource>), 200)]
        [HttpGet("ShopKeeperUser/{PhoneNumber}/Message/{ItemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public async Task<IActionResult> GetShopKeeperUserMessageByIdAsync(GetItemByIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            var response =
                new SingleModelResponse<MinimumInboxMessageResource>() as
                    ISingleModelResponse<MinimumInboxMessageResource>;
            // Unified model for single response.
            response.Model =
                await UnitOfWork.InboxService.GetShopKeeperUserInboxMessageByIdAsync(itemByIdParameters.ItemId, ct);
            if (response.Model == null) return NotFound();
            return Ok(response);
        }
    }
}