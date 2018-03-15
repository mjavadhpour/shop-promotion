// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading;
using System.Threading.Tasks;

namespace ShopPromotion.Domain.Services.InboxServices
{
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Response.Pagination;

    /// <summary>
    /// AppUser Report service.
    /// </summary>
    public interface IInboxService
    {
        /// <summary>
        /// Get a entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<MinimumInboxMessageResource> GetShopInboxMessageByIdAsync(int id, CancellationToken ct);

        /// <summary>
        /// Get all entitys by pagination and filter queries.
        /// </summary>
        /// <param name="entityTypeParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IPage<MinimumInboxResource>> GetShopInboxMessagesAsync(IEntityTypeParameters entityTypeParameters,
            CancellationToken ct);

        /// <summary>
        /// Get a entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<MinimumInboxMessageResource> GetAppUserInboxMessageByIdAsync(int id, CancellationToken ct);

        /// <summary>
        /// Get all entitys by pagination and filter queries.
        /// </summary>
        /// <param name="entityTypeParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IPage<MinimumInboxResource>> GetAppUserInboxMessagesAsync(IEntityTypeParameters entityTypeParameters,
            CancellationToken ct);

        /// <summary>
        /// Get a entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<MinimumInboxMessageResource> GetShopKeeperUserInboxMessageByIdAsync(int id, CancellationToken ct);

        /// <summary>
        /// Get all entitys by pagination and filter queries.
        /// </summary>
        /// <param name="entityTypeParameters"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IPage<MinimumInboxResource>> GetShopKeeperUserInboxMessagesAsync(IEntityTypeParameters entityTypeParameters,
            CancellationToken ct);
    }
}