// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers
{
    // API
    using Infrastructure.Models.Parameter;
    // Domain
    using Domain.Services.PaginationHelper;
    using Domain.EntityLayer;
    using Domain.Infrastructure.Models.Parameter;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response;
    using Domain.Infrastructure.Models.Response.Pagination;
    using Domain.Services;
    // Helper
    using Helper.Infrastructure.ActionResults;

    /// <summary>
    /// Base controller.
    /// </summary>
    public abstract class BaseApiController<TForm, TMinimumTResource, T, TGetAllParameters, TGetItemParameters> : BaseController
        where T : BaseEntity, new()
        where TForm : BaseEntity
        where TMinimumTResource : MinimumBaseEntity
        where TGetAllParameters : IEntityTypeParameters
        where TGetItemParameters : GetItemByIdParameters
    {
        private readonly UserManager<BaseIdentityUser> _userManager;

        /// <summary>
        /// The Base entity service. SHOULD pass by child with real service that was related to API.
        /// </summary>
        protected readonly IBaseService<TForm, TMinimumTResource, T> EntityService;

        /// <summary>
        /// Base controller constructor.
        /// </summary>
        /// <param name="defaultPagingOptionsAccessor"></param>
        /// <param name="entityService"></param>
        /// <param name="userManager"></param>
        protected BaseApiController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            IBaseService<TForm, TMinimumTResource, T> entityService, 
            UserManager<BaseIdentityUser> userManager) : base(defaultPagingOptionsAccessor)
        {
            EntityService = entityService;
            _userManager = userManager;
        }

        /// <summary>
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="entityTypeParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiError), 400)]
        public virtual async Task<IActionResult> GetEntitiesAsync(PagingOptions pagingOptions,
            TGetAllParameters entityTypeParameters, CancellationToken ct)
        {
            var entities = await EntityService.GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);

            var collection = Page<TMinimumTResource>.Create(
                entities.Results.ToArray(),
                entities.TotalNumberOfRecords,
                DefaultPagingOptions);

            return Ok(collection);
        }

        /// <summary>
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{itemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public virtual async Task<IActionResult> GetEntityByIdAsync(TGetItemParameters itemByIdParameters,
            CancellationToken ct)
        {
            var response =
                new SingleModelResponse<TMinimumTResource>() as ISingleModelResponse<TMinimumTResource>;
            // Unified model for single response.
            response.Model = await EntityService.GetEntityAsync(itemByIdParameters.ItemId, ct);
            if (response.Model == null) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// </summary>
        /// <param name="form"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiError), 400)]
        public virtual async Task<IActionResult> CreateEntityAsync(TForm form, CancellationToken ct)
        {
            var response =
                new SingleModelResponse<TMinimumTResource>() as ISingleModelResponse<TMinimumTResource>;
            // Unified model for single response.
            form.CreatedById = GetUserId(HttpContext);
            response.Model = await EntityService.AddEntityAsync(form, ct);
            return CreatedAtAction(nameof(GetEntityByIdAsync), new {itemId = response.Model.Id}, response);
        }

        /// <summary>
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="form"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut("{itemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public virtual async Task<IActionResult> UpdateEntityAsync(TGetItemParameters itemByIdParameters,
            [FromBody] TForm form, CancellationToken ct)
        {
            // Check entity exists or not.
            var entity = GetEntityByIdAsync(itemByIdParameters, ct).Result;
            if (entity.GetType() != typeof(OkObjectResult)) return NotFound();

            form.Id = itemByIdParameters.ItemId;
            await EntityService.UpdateEntityAsync(form, ct);
            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("{itemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public virtual async Task<IActionResult> DeleteEntityAsync(TGetItemParameters itemByIdParameters,
            CancellationToken ct)
        {
            // Check entity exists or not.
            var entity = GetEntityByIdAsync(itemByIdParameters, ct).Result;
            if (entity.GetType() != typeof(OkObjectResult)) return NotFound();

            await EntityService.DeleteEntityAsync(new T {Id = itemByIdParameters.ItemId}, ct);
            return NoContent();
        }

        /// <summary>
        /// Get logged in user ID.
        /// </summary>
        /// <remarks>
        /// For test reason we return zero guid if HttpContext was not registered.
        /// </remarks>
        /// <param name="context"></param>
        /// <returns></returns>
        protected string GetUserId(HttpContext context)
        {
            return context == null ? new Guid().ToString() : _userManager.GetUserId(context.User);
        }
    }
}