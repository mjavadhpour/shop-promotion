// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Services.PaginationHelper
{
    /// <summary>
    /// Resolved value in the filter inside of helper project. <see cref="ShopPromotion.Helper.Infrastructure.Filters.PaginationDefaultValueFilter"/>
    /// </summary>
    public class ResolvedPaginationValueService
    {
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}