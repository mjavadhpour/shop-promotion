// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.ModelLayer.Response.Pagination
{
    public interface IPagingOptions
    {
        string OrderBy { get; set; }

        string Ascending { get; set; }

        int? Page { get; set; }

        int? PageSize { get; set; }
    }
}