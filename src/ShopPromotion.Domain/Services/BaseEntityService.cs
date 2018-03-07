// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Services
{
    using PaginationHelper;

    public abstract class BaseEntityService<TContext>
    {
        protected readonly TContext Context;
        /// <summary>
        /// Resolved velue for pagination.
        /// </summary>
        protected readonly ResolvedPaginationValueService PaginationValues;

        protected BaseEntityService(ResolvedPaginationValueService resolvedPaginationValue, TContext context)
        {
            Context = context;
            PaginationValues = resolvedPaginationValue;
        }
    }
}