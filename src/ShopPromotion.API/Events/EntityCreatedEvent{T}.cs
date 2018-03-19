// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using MediatR;
using ShopPromotion.Domain.EntityLayer;
using ShopPromotion.Domain.Infrastructure.Models.Resource;

namespace ShopPromotion.API.Events
{
    public class EntityCreatedEvent<TMinimumTResource, TForm> : INotification
        where TMinimumTResource : MinimumBaseEntity where TForm : BaseEntity
    {
        public readonly TMinimumTResource MinimumResource;
        public readonly TForm Form;

        public EntityCreatedEvent(TMinimumTResource minimumResource, TForm form)
        {
            MinimumResource = minimumResource;
            Form = form;
        }
    }
}