// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.Extensions.Options;

namespace ShopPromotion.Domain.ModelLayer.Response.Pagination
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Page class for pagination.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class Page<TModel> : IPage<TModel>
    {
#pragma warning disable CA1000
        private static PagingOptions DefaultPagingOptionsTest { get; set; }
#pragma warning restore CA1000

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="defaultPagingOptionsAccessor"></param>
        protected Page(IOptions<PagingOptions> defaultPagingOptionsAccessor)
        {
            DefaultPagingOptionsTest = defaultPagingOptionsAccessor.Value;
        }

        private Page()
        { }

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
        /// <param name="items"></param>
        /// <param name="totalItemsNumber"></param>
        /// <returns></returns>
        public static Page<TModel> Create(TModel[] items, int totalItemsNumber)
        {
            return new Page<TModel>
            {
                Results = items,
                TotalNumberOfRecords = totalItemsNumber,
                TotalNumberOfPages = totalItemsNumber % DefaultPagingOptionsTest.PageSize > 0
                    ? totalItemsNumber / DefaultPagingOptionsTest.PageSize + 1
                    : totalItemsNumber / DefaultPagingOptionsTest.PageSize,
                PageSize = DefaultPagingOptionsTest.PageSize,
                PageNumber = DefaultPagingOptionsTest.Page
            };
        }
    }
}