// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.AppSettings
{
    using Models.Response.Pagination;

    /// <summary>
    /// App settings mapping object to use in DI for Domain base settings.
    /// </summary>
    public class ShopPromotionDomainAppSettings
    {
        public ConnectionString ConnectionStrings { get; set; }

        public PagingOptions PagingOptions { get; set; }
    }
}