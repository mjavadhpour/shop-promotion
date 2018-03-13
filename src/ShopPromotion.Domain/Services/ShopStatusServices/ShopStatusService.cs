// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services.ShopStatusServices
{   
    using EntityLayer;
    using Exceptions;
    using Infrastructure;
    using Infrastructure.Models.Form;
    using PaginationHelper;

    /// <inheritdoc cref="IShopStatusService" />
    public class ShopStatusService : BaseEntityService<ShopPromotionDomainContext>, IShopStatusService
    {
        /// <inheritdoc />
        public ShopStatusService(ResolvedPaginationValueService resolvedPaginationValue,
            ShopPromotionDomainContext context) : base(resolvedPaginationValue, context)
        {
        }

        /// <inheritdoc />
        public async Task ChangeShopStatus(IShopStatusForm shopStatusForm, CancellationToken ct)
        {
            var shop = await Context.Shops.AsNoTracking().SingleOrDefaultAsync(x => x.Id == shopStatusForm.ShopId, ct);
            if (shop == null) throw new ShopNotFoundException();
            var shopStatus = new ShopStatus
            {
                CreatedAt = DateTime.Now,
                ShopId = shop.Id,
                Status = shopStatusForm.StatusOptions
            };
            Context.Set<ShopStatus>().Add(shopStatus);
        }
    }
}