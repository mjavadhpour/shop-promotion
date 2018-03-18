// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopPromotion.Domain.Infrastructure.Models.Form;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Extensions;
    using Infrastructure;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using PaginationHelper;

    public class
        DefaultDiscountService<T> : DefaultEntityService<T, MinimumDiscountListResource, MinimumDiscountResource, Discount,
            ShopPromotionDomainContext>
        where T : DiscountCreateForm
    {
        public DefaultDiscountService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<Discount> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<Discount> GetElementsOfTModelSequenceAsync(IEntityTypeParameters entityTypeParameters)
        {
            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override Discount MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            DiscountCreateForm finalForm;
            // In update client can just change the enabled field.
            if (GetCurrentAction() == UpdateEntity)
            {
                finalForm = Mapper.Map<DiscountCreateForm>(CurrentFindedObject);
                finalForm.Enabled = form.Enabled;
            }
            else
            {
                // In create we resolve final form with given create form from application layer.
                finalForm = form;
            }

            // Map discount and resource.
            var discount = Mapper.Map<Discount>(finalForm);

            return discount;
        }

        /// <inheritdoc />
        protected override void ValidateAddOrUpdateRequest(T form)
        {
        }
    }
}