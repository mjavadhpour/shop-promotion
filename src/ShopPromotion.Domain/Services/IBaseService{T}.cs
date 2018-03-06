// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Response.Pagination;
    
    /// <summary>
    /// Base service for implement in BaseEntityService.
    /// </summary>
    public interface IBaseService<TForm, TEntityResource, in TEntityModel>
        where TEntityModel : BaseEntity where TForm : BaseEntity
    {
        /// <summary>
        /// Get a entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<TEntityResource> GetEntityAsync(int id, CancellationToken ct);

        /// <summary>
        /// Get all entitys by pagination and filter queries.
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="entityTypeParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IPage<TEntityResource>> GetEntitiesAsync(IPagingOptions pagingOptions,
            IEntityTypeParameters entityTypeParameters,
            CancellationToken ct);

        /// <summary>
        /// Create a entity.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<TEntityResource> AddEntityAsync(TForm form, CancellationToken ct);

        /// <summary>
        /// Update a entity.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task UpdateEntityAsync(TForm form, CancellationToken ct);

        /// <summary>
        /// Delete a entity.
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task DeleteEntityAsync(TEntityModel changes, CancellationToken ct);
    }
}