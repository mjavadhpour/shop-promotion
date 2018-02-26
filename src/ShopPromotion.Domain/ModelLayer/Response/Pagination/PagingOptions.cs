// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.ModelLayer.Response.Pagination
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public class PagingOptions : IPagingOptions
    {
        public const int MaxItemPerPage = 100;

        [FromQuery]
        public string OrderBy { get; set; }

        [FromQuery]
        public string Ascending { get; set; }

        [FromQuery]
        public int? Page { get; set; }

        [FromQuery]
        [Range(1, MaxItemPerPage, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
        public int? PageSize { get; set; }

        public int GetSkipValue(IPagingOptions currentPagingOptions, IPagingOptions defaultValues)
        {
            // Skipped value is deferent from current page (minos one), then we must to separated it to two values.
            Page = currentPagingOptions.Page ?? defaultValues.Page ?? 0;
            return (int) (Page != 0 ? Page - 1 : 0);
        }

        public int GetTakeValue(IPagingOptions currentPagingOptions, IPagingOptions defaultValues)
        {
            return (int) (PageSize = currentPagingOptions.PageSize ?? defaultValues.PageSize ?? MaxItemPerPage);
        }

        public string GetOrderBy(IPagingOptions currentPagingOptions, IPagingOptions defaultValues)
        {
            return OrderBy = currentPagingOptions.OrderBy ?? defaultValues.OrderBy ?? "id";
        }

        public bool GetAscending(IPagingOptions currentPagingOptions, IPagingOptions defaultValues)
        {
            Ascending = currentPagingOptions.Ascending ?? defaultValues.Ascending ?? "ASC";
            return Ascending == "ASC";
        }
    }
}