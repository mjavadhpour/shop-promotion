// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Extensions;
    using Infrastructure.AppSettings;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Response.Pagination;
    // Helper
    using PaginationHelper;

    public class DefaultEntityService<TForm, TModelResource, TModel, TContext> : BaseEntityService,
        IBaseService<TForm, TModelResource, TModel> where TModel : BaseEntity
        where TForm : BaseEntity
        where TContext : DbContext
    {
        protected readonly TContext Context;
        protected readonly DbSet<TModel> Entities;
        protected IQueryable<TModel> Query;
        /// <summary>
        /// Resolved velue for pagination.
        /// </summary>
        private readonly ResolvedPaginationValueService _paginationValue;
        /// <summary>
        /// Hold the actions that specify what actually happening in this class.
        /// </summary>
        private short _currentAction;

        /// <summary>
        /// The actions that will useing in <see cref="_currentAction"/>
        /// </summary>
        internal const short Initialize = 1; 
        internal const short GetEntity = 2; 
        internal const short GetEntities = 3; 
        internal const short AddEntity = 4; 
        internal const short UpdateEntity = 5; 
        internal const short DeleteEntity = 6;

        public DefaultEntityService(
            IOptions<ShopPromotionDomainAppSettings> appSettings, 
            TContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(appSettings)
        {
            Context = context;
            Entities = Context.Set<TModel>();
            Query = Entities;
            _paginationValue = resolvedPaginationValue;
            _currentAction = Initialize;
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public async Task<TModelResource> GetEntityAsync(int id, CancellationToken ct)
        {
            _currentAction = GetEntity;
            var entity = await GetElementOfTModelSequenceAsync(id, ct);

            return Mapper.Map<TModelResource>(entity);
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public virtual async Task<IPage<TModelResource>> GetEntitiesAsync(IPagingOptions pagingOptions,
            IEntityTypeParameters entityTypeParameters, CancellationToken ct)
        {
            _currentAction = GetEntities;
            var totalNumberOfRecords = await Query.CountAsync(ct);
            var results = await GetElementsOfTModelSequenceAsync(
                _paginationValue.PageSize, 
                _paginationValue.Page,
                _paginationValue.OrderBy, 
                _paginationValue.Ascending, ct);

            return Page<TModelResource>.Create(results, totalNumberOfRecords, _paginationValue);
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public async Task<TModelResource> AddEntityAsync(TForm form, CancellationToken ct)
        {
            _currentAction = AddEntity;
            // Set create at field.
            form.CreatedAt = DateTime.Now;

            // Custom validation and thrown intended exceptions
            ValidateAddOrUpdateRequest(form);

            // Map model and resource.
            var model = MappingFromModelToTModelDestination(form, ct);
            Context.Set<TModel>().Add(model);

            await Context.SaveChangesAsync(ct);
            // Fill new generated id.
            form.Id = model.Id;

            return Mapper.Map<TModelResource>(form);
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public async Task UpdateEntityAsync(TForm form, CancellationToken ct)
        {
            _currentAction = UpdateEntity;
            var entity = await GetEntityAsync(form.Id, ct);
            if (entity == null) return;
            // Custom validation and thrown intended exceptions
            ValidateAddOrUpdateRequest(form);

            // Map to real entity object.
            var mappedEntity = MappingFromModelToTModelDestination(form, ct);
            // Change update at field to date now.
            mappedEntity.UpdatedAt = DateTime.Now;

            Context.Set<TModel>().Update(mappedEntity);
            await Context.SaveChangesAsync(ct);
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public async Task DeleteEntityAsync(TModel changes, CancellationToken ct)
        {
            _currentAction = DeleteEntity;
            var modelDto = await GetEntityAsync(changes.Id, ct);
            if (modelDto == null) return;

            // Map model and resource.
            var model = Mapper.Map<TModel>(modelDto);

            Context.Set<TModel>().Remove(model);
            await Context.SaveChangesAsync(ct);
        }

        /// <summary>
        /// Method to provide override facilities for child to use Include method.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected virtual async Task<TModel> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <summary>
        /// Method to provide override facilities for child to use Include method for list of items.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected virtual async Task<TModelResource[]> GetElementsOfTModelSequenceAsync(int pageSize, int pageNumber,
            string orderBy, bool ascending, CancellationToken ct)
        {
            return await Query
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ProjectTo<TModelResource>()
                .ToArrayAsync(ct);
        }

        /// <summary>
        /// Method to provide override facilities for child to use in creat base object.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected virtual TModel MappingFromModelToTModelDestination(TForm form, CancellationToken ct)
        {
            return Mapper.Map<TModel>(form);
        }

        /// <summary>
        /// Custom validation method to thrown intended exceptions in <see cref="AddEntityAsync"/> and
        /// <see cref="UpdateEntityAsync"/> 
        /// </summary>
        /// <remarks>
        /// This function run before commit the real query into database.
        /// </remarks>
        /// <param name="form"></param>
        protected virtual void ValidateAddOrUpdateRequest(TForm form)
        {}

        /// <summary>
        /// Get the actions that specify what actually happening in <see cref="DefaultEntityService{TForm,TModelResource,TModel,TContext}"/>.
        /// </summary>
        /// <returns> Currently action alias. </returns>
        protected short GetCurrentAction()
        {
            return _currentAction;
        }
    }
}