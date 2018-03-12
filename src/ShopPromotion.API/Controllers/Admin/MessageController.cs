﻿// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
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
    using ServiceConfiguration;
    // Doamin
    using Domain.EntityLayer;
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Form;
    using Domain.Infrastructure.Models.Response;
    using Domain.Infrastructure.Models.Parameter;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response.Pagination;
    using Domain.Services.PaginationHelper;

    /// <summary>
    /// Message controller.
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class MessageController : BaseApiController<MessageForm, MinimumMessageListResource, MinimumMessageResource, 
        Message, GetAllMessagesParameters, GetItemByIdParameters>
    {
        /// <inheritdoc />
        public MessageController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork,
            UserManager<BaseIdentityUser> userManager,
            UnitOfWork<MessageForm, MinimumMessageListResource, MinimumMessageResource, Message> genericUnitOfWork) 
            : base(defaultPagingOptionsAccessor, unitOfWork, userManager, genericUnitOfWork)
        {
        }

        /// Get list of messages.
        /// <summary>
        /// Get list of messages.
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
        [ProducesResponseType(typeof(Page<MinimumMessageListResource>), 200)]
        public override Task<IActionResult> GetEntitiesAsync(PagingOptions pagingOptions,
            GetAllMessagesParameters entityTypeParameters, CancellationToken ct)
        {
            return base.GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);
        }

        /// Get a message by id.
        /// <summary>
        /// Get a message by id.
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
        [ProducesResponseType(typeof(SingleModelResponse<MinimumMessageResource>), 200)]
        public override Task<IActionResult> GetEntityByIdAsync(GetItemByIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.GetEntityByIdAsync(itemByIdParameters, ct);
        }

        /// Create new message.
        /// <summary>
        /// Create new message.
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
        [ProducesResponseType(typeof(SingleModelResponse<MinimumMessageResource>), 201)]
        public override Task<IActionResult> CreateEntityAsync([FromBody] MessageForm form, CancellationToken ct)
        {
            // TODO: Create target point to the requested message.
            return base.CreateEntityAsync(form, ct);
        }

        /// Update existing message.
        /// <summary>
        /// Update existing message.
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
        public override Task<IActionResult> UpdateEntityAsync(GetItemByIdParameters itemByIdParameters,
            [FromBody] MessageForm form, CancellationToken ct)
        {
            return base.UpdateEntityAsync(itemByIdParameters, form, ct);
        }

        /// Delete existing message.
        /// <summary>
        /// Delete existing message.
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
        public override Task<IActionResult> DeleteEntityAsync(GetItemByIdParameters itemByIdParameters,
            CancellationToken ct)
        {
            return base.DeleteEntityAsync(itemByIdParameters, ct);
        }
    }
}