// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Response.Pagination
{
    using System.Collections.Generic;

    public interface IPage<TModel> : IResponse
    {
        /// <summary>
        /// The page number this page represents.
        /// </summary>
        int? PageNumber { get; set; }

        /// <summary>
        /// The size of this page.
        /// </summary>
        int? PageSize { get; set; }

        /// <summary>
        /// The total number of pages available.
        /// </summary>
        int? TotalNumberOfPages { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        int TotalNumberOfRecords { get; set; }

        /// <summary>
        /// The URL to the next page - if null, there are no more pages.
        /// </summary>
        string NextPageUrl { get; set; }

        /// <summary>
        /// The records this page represents.
        /// </summary>
        IEnumerable<TModel> Results { get; set; }

        /// <summary>
        /// The paging options that generated in service.
        /// </summary>
        IPagingOptions PagingOptions { get; set; }
    }
}