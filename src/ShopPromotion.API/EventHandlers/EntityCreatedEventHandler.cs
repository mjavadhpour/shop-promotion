// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopPromotion.API.Infrastructure.Models.Form;
using ShopPromotion.Domain.Infrastructure;

namespace ShopPromotion.API.EventHandlers
{
    // API
    using Events;
    // Domain
    using Domain.Infrastructure.Models.Resource;

    public class
        EntityCreatedEventHandler : INotificationHandler<
            EntityCreatedEvent<MinimumShopCheckoutRequestResource, ShopCheckoutRequestForm>>
    {
        private readonly ShopPromotionDomainContext _context;

        public EntityCreatedEventHandler(ShopPromotionDomainContext context)
        {
            _context = context;
        }

        public async Task Handle(EntityCreatedEvent<MinimumShopCheckoutRequestResource, ShopCheckoutRequestForm> notification,
            CancellationToken cancellationToken)
        {
            string queryString =
                $@"INSERT INTO [ShopCheckoutRequestOrders] (CreatedAt, UpdatedAt, CreatedById, ShopCheckoutRequestId, OrderId) 
                 SELECT '{DateTime.Now}', '{DateTime.Now}', '{notification.Form.CreatedById}', '{
                        notification.MinimumResource.CheckoutRequestId
                    }', o.Id
                 FROM [Orders] o
                 WHERE o.CreatedAt >= '{notification.Form.FromDate}' AND o.CreatedAt <= '{
                        notification.Form.ToDate
                    }' AND o.Id NOT IN (SELECT OrderId FROM [ShopCheckoutRequestOrders])";

            await _context.Database.ExecuteSqlCommandAsync(queryString, cancellationToken);
        }
    }
}