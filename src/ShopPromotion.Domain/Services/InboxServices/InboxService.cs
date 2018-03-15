// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services.InboxServices
{
    using EntityLayer;
    using Extensions;
    using Infrastructure;
    using PaginationHelper;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Response.Pagination;

    /// <inheritdoc cref="IInboxService" />
    public class InboxService : BaseEntityService<ShopPromotionDomainContext>, IInboxService
    {
        public InboxService(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context) : base(resolvedPaginationValue, context)
        {
        }

        /// <inheritdoc />
        public async Task<MinimumInboxMessageResource> GetShopInboxMessageByIdAsync(int id, CancellationToken ct)
        {
            var query = Context.Set<ShopInbox>();
            var entity = await query
                .AsNoTracking()
                .Include(e => e.Message)
                .SingleOrDefaultAsync(x => x.Id == id, ct);

            return Mapper.Map<MinimumInboxMessageResource>(entity);
        }

        /// <inheritdoc />
        public async Task<IPage<MinimumInboxResource>> GetShopInboxMessagesAsync(
            IEntityTypeParameters entityTypeParameters,
            CancellationToken ct)
        {
            var query = GetEntitiesPaginateQueryable<ShopInbox>();

            // Filter by shop. required.
            query = query.Where(si => si.ShopId == (int) entityTypeParameters.GetParameter("ShopId"));
            
            var results = await query
                .Include(si => si.Message)
                .ProjectTo<MinimumInboxResource>()
                .ToArrayAsync(ct);

            var totalNumberOfRecords = await query.CountAsync(ct);

            return Page<MinimumInboxResource>.Create(results, totalNumberOfRecords, PaginationValues);
        }

        /// <inheritdoc />
        public async Task<MinimumInboxMessageResource> GetAppUserInboxMessageByIdAsync(int id, CancellationToken ct)
        {
            var query = Context.Set<AppUserInbox>();
            var entity = await query
                .AsNoTracking()
                .Include(e => e.Message)
                .SingleOrDefaultAsync(x => x.Id == id, ct);

            return Mapper.Map<MinimumInboxMessageResource>(entity);
        }

        /// <inheritdoc />
        public async Task<IPage<MinimumInboxResource>> GetAppUserInboxMessagesAsync(
            IEntityTypeParameters entityTypeParameters,
            CancellationToken ct)
        {
            var query = GetEntitiesPaginateQueryable<AppUserInbox>();

            // Filter by user. required.
            query = query.Where(ai =>
                ai.AppUser.PhoneNumber == (string) entityTypeParameters.GetParameter("PhoneNumber"));

            var results = await query
                .Include(aui => aui.Message)
                .ProjectTo<MinimumInboxResource>()
                .ToArrayAsync(ct);

            var totalNumberOfRecords = await query.CountAsync(ct);

            return Page<MinimumInboxResource>.Create(results, totalNumberOfRecords, PaginationValues);
        }

        /// <inheritdoc />
        public async Task<MinimumInboxMessageResource> GetShopKeeperUserInboxMessageByIdAsync(int id,
            CancellationToken ct)
        {
            var query = Context.Set<ShopKeeperUserInbox>();

            var entity = await query
                .AsNoTracking()
                .Include(e => e.Message)
                .SingleOrDefaultAsync(x => x.Id == id, ct);

            return Mapper.Map<MinimumInboxMessageResource>(entity);
        }

        /// <inheritdoc />
        public async Task<IPage<MinimumInboxResource>> GetShopKeeperUserInboxMessagesAsync(
            IEntityTypeParameters entityTypeParameters, CancellationToken ct)
        {
            var query = GetEntitiesPaginateQueryable<ShopKeeperUserInbox>();

            // Filter by user. required.
            query = query.Where(ski =>
                ski.ShopKeeperUser.PhoneNumber == (string) entityTypeParameters.GetParameter("PhoneNumber"));

            var results = await query
                .Include(skui => skui.Message)
                .ProjectTo<MinimumInboxResource>()
                .ToArrayAsync(ct);

            var totalNumberOfRecords = await query.CountAsync(ct);

            return Page<MinimumInboxResource>.Create(results, totalNumberOfRecords, PaginationValues);
        }

        /// <summary>
        /// Create get entities query.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        private IQueryable<TEntity> GetEntitiesPaginateQueryable<TEntity>()
            where TEntity : BaseEntity
        {
            return Context.Set<TEntity>()
                .OrderByPropertyOrField(PaginationValues.OrderBy, PaginationValues.Ascending)
                .Skip(PaginationValues.Page * PaginationValues.PageSize)
                .Take(PaginationValues.PageSize);
        }
    }
}