// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Infrastructure;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using PaginationHelper;

    public class
        DefaultShopService<T> : DefaultEntityService<T, MinimumShopListResource, MinimumShopResource, Shop,
            ShopPromotionDomainContext>
        where T : BaseEntity
    {
        public DefaultShopService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<Shop> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .Include(shop => shop.ShopAddresses)
                .Include(shop => shop.ShopAttributes)
                .ThenInclude(st => st.Attribute)
                .Include(shop => shop.ShopGeolocation)
                .Include(shop => shop.ShopImages)
                .Include(shop => shop.ShopStatuses)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<Shop> GetElementsOfTModelSequenceAsync(IEntityTypeParameters entityTypeParameters)
        {
            Query = Query
                .Include(shop => shop.ShopImages)
                .Include(shop => shop.ShopAttributes)
                .Where(shop =>
                    shop.ShopStatuses.Where(ss => ss.Status == ShopStatusOption.Approved).OrderByDescending(ss => ss.Id)
                        .FirstOrDefault().Id == shop.ShopStatuses.OrderByDescending(ss => ss.Id).FirstOrDefault().Id);

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override Shop MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map shop and resource.
            var shop = Mapper.Map<Shop>(form);

            // Assign default status to new shop.
            if (GetCurrentAction() == CreateEntity)
            {
                Context.Set<ShopStatus>().Add(new ShopStatus
                {
                    CreatedAt = DateTime.Now,
                    Shop = shop
                });
            }

            return shop;
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