// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Response.Pagination
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
    }
}