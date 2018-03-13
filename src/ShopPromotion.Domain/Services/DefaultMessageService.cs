// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Extensions;
    using Infrastructure;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Response.Pagination;
    using PaginationHelper;

    public class
        DefaultMessageService<T> : DefaultEntityService<T, MinimumMessageListResource, MinimumMessageResource, Message, ShopPromotionDomainContext>
        where T : BaseEntity
    {
        public DefaultMessageService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<Message> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .Include(m => m.Author)
                .Include(m => m.MessageTargets)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }
        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<MinimumMessageListResource[]> GetElementsOfTModelSequenceAsync(int pageSize,
            int pageNumber, string orderBy, bool ascending,
            CancellationToken ct)
        {
            return await Query
                .OrderByPropertyOrField(orderBy, ascending)
                .Include(m => m.Author)
                .Include(m => m.MessageTargets)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ProjectTo<MinimumMessageListResource>()
                .ToArrayAsync(ct);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override Message MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map message and resource.
            var message = Mapper.Map<Message>(form);

            return message;
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        public override async Task<IPage<MinimumMessageListResource>> GetEntitiesAsync(IPagingOptions pagingOptions,
            IEntityTypeParameters entityTypeParameters, CancellationToken ct)
        {
            return await base.GetEntitiesAsync(pagingOptions, entityTypeParameters, ct);
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if shop have duplicated size for each product group thrown error.
        /// </summary>
        protected override void ValidateAddOrUpdateRequest(T form)
        {
        }
    }
}