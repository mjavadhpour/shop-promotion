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

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Extensions;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.Response.Pagination;
    // Helper
    using PaginationHelper;

    public class DefaultEntityService<TForm, TListModelResource, TModelResource, TModel, TContext> : BaseEntityService<TContext>,
        IBaseService<TForm, TListModelResource, TModelResource, TModel, TContext> where TModel : BaseEntity
        where TForm : BaseEntity
        where TContext : DbContext
        where TListModelResource : MinimumBaseEntity
        where TModelResource : MinimumBaseEntity
    {
        protected readonly DbSet<TModel> Entities;
        protected TModel CurrentFindedObject { get; private set; }
        protected IQueryable<TModel> Query;
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
        internal const short CreateEntity = 4; 
        internal const short UpdateEntity = 5; 
        internal const short DeleteEntity = 6;

        public DefaultEntityService( 
            TContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(resolvedPaginationValue, context)
        {
            Entities = Context.Set<TModel>();
            Query = Entities;
            _currentAction = Initialize;
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public async Task<TModelResource> GetEntityAsync(int id, CancellationToken ct)
        {
            _currentAction = GetEntity;
            CurrentFindedObject = await GetElementOfTModelSequenceAsync(id, ct);

            return Mapper.Map<TModelResource>(CurrentFindedObject);
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public async Task<IPage<TListModelResource>> GetEntitiesAsync(IEntityTypeParameters entityTypeParameters, 
            CancellationToken ct)
        {
            _currentAction = GetEntities;
            var results = await GetElementsOfTModelSequenceAsync(entityTypeParameters)
                .OrderByPropertyOrField(PaginationValues.OrderBy, PaginationValues.Ascending)
                .Skip(PaginationValues.Page * PaginationValues.PageSize)
                .Take(PaginationValues.PageSize)
                .ProjectTo<TListModelResource>()
                .ToArrayAsync(ct);

            var totalNumberOfRecords = await Query.CountAsync(ct);

            return Page<TListModelResource>.Create(results, totalNumberOfRecords, PaginationValues);
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public TModelResource AddEntity(TForm form, CancellationToken ct)
        {
            _currentAction = CreateEntity;
            // Set create at field.
            form.CreatedAt = DateTime.Now;

            // Custom validation and thrown intended exceptions
            ValidateAddOrUpdateRequest(form);

            // Map model and resource.
            var model = MappingFromModelToTModelDestination(form, ct);
            Context.Set<TModel>().Add(model);

            return Mapper.Map<TModelResource>(form);
        }

        /// <inheritdoc>
        /// <cref>IBaseService{TForm, TEntityResource,TEntityModel}</cref>
        /// </inheritdoc>
        public async Task UpdateEntityAsync(TForm form, CancellationToken ct)
        {
            var entity = await GetEntityAsync(form.Id, ct);
            _currentAction = UpdateEntity;
            if (entity == null) return;
            // Custom validation and thrown intended exceptions
            ValidateAddOrUpdateRequest(form);

            // Map to real entity object.
            var mappedEntity = MappingFromModelToTModelDestination(form, ct);
            // Fill Old CreatedAt
            mappedEntity.CreatedAt = entity.CreatedAt;
            Context.Set<TModel>().Update(mappedEntity);
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
        /// <returns></returns>
        protected virtual IQueryable<TModel> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            return Query;
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
        /// Custom validation method to thrown intended exceptions in <see cref="CreateEntity"/> and
        /// <see cref="UpdateEntityAsync"/> 
        /// </summary>
        /// <remarks>
        /// This function run before commit the real query into database.
        /// </remarks>
        /// <param name="form"></param>
        protected virtual void ValidateAddOrUpdateRequest(TForm form)
        {}

        /// <summary>
        /// Get the actions that specify what actually happening in <see cref="DefaultEntityService{TForm,TListModelResource, TModelResource,TModel,TContext}"/>.
        /// </summary>
        /// <returns> Currently action alias. </returns>
        protected short GetCurrentAction()
        {
            return _currentAction;
        }
    }
}