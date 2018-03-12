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
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Parameter;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response;
    using Domain.Infrastructure.Models.Response.Pagination;
    // Helper
    using Helper.Infrastructure.ActionResults;

    /// <summary>
    /// Base controller.
    /// </summary>
    public abstract class BaseApiController<TForm, TMinimumTListResource, TMinimumTResource, T, TGetAllParameters, TGetItemParameters> : BaseController
        where T : BaseEntity, new()
        where TForm : BaseEntity
        where TMinimumTListResource: MinimumBaseEntity
        where TMinimumTResource : MinimumBaseEntity
        where TGetAllParameters : IEntityTypeParameters
        where TGetItemParameters : GetItemByIdParameters
    {
        private readonly UserManager<BaseIdentityUser> _userManager;

        /// <summary>
        /// The Base entity service. SHOULD pass by child with real service that was related to API.
        /// </summary>
        protected readonly UnitOfWork<TForm, TMinimumTListResource, TMinimumTResource, T> GenericUnitOfWork;

        /// <summary>
        /// Base controller constructor.
        /// </summary>
        /// <param name="defaultPagingOptionsAccessor"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="userManager"></param>
        /// <param name="genericUnitOfWork"></param>
        protected BaseApiController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork,
            UserManager<BaseIdentityUser> userManager, UnitOfWork<TForm, TMinimumTListResource, TMinimumTResource, T> genericUnitOfWork) :
            base(defaultPagingOptionsAccessor, unitOfWork)
        {
            _userManager = userManager;
            GenericUnitOfWork = genericUnitOfWork;
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
            var entities = await GenericUnitOfWork.GenericRepository()
                .GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);

            var collection = Page<TMinimumTListResource>.Create(
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
        [HttpGet("{ItemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public virtual async Task<IActionResult> GetEntityByIdAsync(TGetItemParameters itemByIdParameters,
            CancellationToken ct)
        {
            var response =
                new SingleModelResponse<TMinimumTResource>() as ISingleModelResponse<TMinimumTResource>;
            // Unified model for single response.
            response.Model = await GenericUnitOfWork.GenericRepository().GetEntityAsync(itemByIdParameters.ItemId, ct);
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
            response.Model = GenericUnitOfWork.GenericRepository().AddEntity(form, ct);
            await GenericUnitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetEntityByIdAsync), new {ItemId = response.Model.Id}, response);
        }

        /// <summary>
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="form"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut("{ItemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public virtual async Task<IActionResult> UpdateEntityAsync(TGetItemParameters itemByIdParameters,
            [FromBody] TForm form, CancellationToken ct)
        {
            // Check entity exists or not.
            var entity = GetEntityByIdAsync(itemByIdParameters, ct).Result;
            if (entity.GetType() != typeof(OkObjectResult)) return NotFound();

            form.Id = itemByIdParameters.ItemId;
            await GenericUnitOfWork.GenericRepository().UpdateEntityAsync(form, ct);
            await GenericUnitOfWork.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <param name="itemByIdParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("{ItemId}")]
        [ProducesResponseType(typeof(ApiError), 400)]
        public virtual async Task<IActionResult> DeleteEntityAsync(TGetItemParameters itemByIdParameters,
            CancellationToken ct)
        {
            // Check entity exists or not.
            var entity = GetEntityByIdAsync(itemByIdParameters, ct).Result;
            if (entity.GetType() != typeof(OkObjectResult)) return NotFound();

            await GenericUnitOfWork.GenericRepository().DeleteEntityAsync(new T {Id = itemByIdParameters.ItemId}, ct);
            await GenericUnitOfWork.SaveAsync();
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