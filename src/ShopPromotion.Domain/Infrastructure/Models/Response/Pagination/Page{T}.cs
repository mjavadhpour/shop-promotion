// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopPromotion.Domain.Infrastructure.Models.Response.Pagination
{
    // Domain 
    using Services.PaginationHelper;

    /// <summary>
    /// Page class for pagination.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class Page<TModel> : IPage<TModel>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PageNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PageSize { get; set; }

        public int? TotalNumberOfPages { get; set; }

        public int TotalNumberOfRecords { get; set; }

        [JsonIgnore]
        public string NextPageUrl { get; set; }

        public IEnumerable<TModel> Results { get; set; }

        [JsonIgnore]
        public IPagingOptions PagingOptions { get; set; }

        /// <summary>
        /// Create base pageination.
        /// </summary>
        /// <remarks>
        /// In the static generic objects we can not use DI,
        /// because DI work in compile time and static context work in runtime!
        /// </remarks>
        /// <param name="items"></param>
        /// <param name="totalItemsNumber"></param>
        /// <param name="defaultPagingOptions"></param>
        /// <returns></returns>
        public static Page<TModel> Create(TModel[] items, int totalItemsNumber,
            ResolvedPaginationValueService defaultPagingOptions)
        {
            return new Page<TModel>
            {
                Results = items,
                TotalNumberOfRecords = totalItemsNumber,
                TotalNumberOfPages = totalItemsNumber % defaultPagingOptions.PageSize > 0
                    ? totalItemsNumber / defaultPagingOptions.PageSize + 1
                    : totalItemsNumber / defaultPagingOptions.PageSize,
                PageSize = defaultPagingOptions.PageSize,
                PageNumber = defaultPagingOptions.Page + 1
            };
        }
    }
}