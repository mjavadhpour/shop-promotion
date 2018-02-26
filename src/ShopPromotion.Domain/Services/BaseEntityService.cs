// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.Extensions.Options;

namespace ShopPromotion.Domain.Services
{
    using Infrastructure.AppSettings;
    using ModelLayer.Response.Pagination;

    public abstract class BaseEntityService
    {
        protected readonly IPagingOptions DefaultPagingOptions;

        protected BaseEntityService(IOptions<ShopPromotionDomainAppSettings> appSettings)
        {
            DefaultPagingOptions = appSettings.Value.PagingOptions;
        }
    }
}