// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Exceptions;
    using Infrastructure;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using PaginationHelper;

    public class
        DefaultAdminMessageService<T> : DefaultEntityService<T, MinimumMessageListResource, MinimumMessageResource, Message, ShopPromotionDomainContext>
        where T : MessageForm
    {
        public DefaultAdminMessageService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<Message> GetElementOfTModelSequenceAsync(int id, CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .Include(m => m.Author)
                .Include(m => m.MessageTargets)
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<Message> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            // Filter by target.
            var requestedTarget = (MessageTargetTypeOption) entityTypeParameters.GetParameter("Target");
            switch (requestedTarget)
            {
                case MessageTargetTypeOption.All:
                    Query = Query.Where(x => x.MessageTargets.Any(mt =>
                        mt.TargetType == MessageTargetTypeOption.All && mt.TargetObjectId == null));
                    break;
                case MessageTargetTypeOption.AppUser:
                    Query = Query.Where(x => x.MessageTargets.Any(mt =>
                        mt.TargetType == MessageTargetTypeOption.AppUser && mt.TargetObjectId == null));
                    break;
                case MessageTargetTypeOption.Shop:
                    Query = Query.Where(x => x.MessageTargets.Any(mt =>
                        mt.TargetType == MessageTargetTypeOption.Shop && mt.TargetObjectId == null));
                    break;
                case MessageTargetTypeOption.ShopKeeper:
                    Query = Query.Where(x => x.MessageTargets.Any(mt =>
                        mt.TargetType == MessageTargetTypeOption.ShopKeeper && mt.TargetObjectId == null));
                    break;
                default:
                    Query = Query.Where(x => x.MessageTargets.Any(mt => mt.TargetObjectId == null));
                    break;
            }

            Query = Query
                .Include(m => m.Author)
                .Include(m => m.MessageTargets);

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override Message MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map message and resource.
            var message = Mapper.Map<Message>(form);

            var messageTraget = new MessageTarget
            {
                CreatedAt = DateTime.Now,
                TargetType = form.MessageTargetType,
                TargetObjectId = form.TargetObjectId,
                CreatedById = message.AuthorId
            };
            // Assign message target.
            message.MessageTargets = new List<MessageTarget> {messageTraget};

            switch (form.MessageTargetType)
            {
                case MessageTargetTypeOption.AppUser:
                    if (form.TargetObjectId != null)
                    {
                        var user = Context.Set<AppUser>().AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Id == form.TargetObjectId, ct);
                        if (user.Result == null) throw new UserNotFoundException();
                        var appUserInbox = new AppUserInbox
                        {
                            AppUserId = user.Result.Id,
                            CreatedAt = DateTime.Now,
                            Message = message
                        };
                        Context.Set<AppUserInbox>().Add(appUserInbox);
                    }

                    break;
                case MessageTargetTypeOption.Shop:
                    if (form.TargetObjectId != null)
                    {
                        var shop = Context.Set<Shop>().AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Id == Int32.Parse(form.TargetObjectId), ct);
                        if (shop.Result == null) throw new ShopNotFoundException();
                        var shopInbox = new ShopInbox
                        {
                            CreatedAt = DateTime.Now,
                            ShopId = shop.Result.Id,
                            Message = message
                        };
                        Context.Set<ShopInbox>().Add(shopInbox);
                    }
                    break;
                case MessageTargetTypeOption.ShopKeeper:
                    if (form.TargetObjectId != null)
                    {
                        var shopKeeper = Context.Set<ShopKeeperUser>().AsNoTracking()
                            .SingleOrDefaultAsync(x => x.Id == form.TargetObjectId, ct);
                        if (shopKeeper.Result == null) throw new UserNotFoundException();
                        var shopKeeperInbox = new ShopKeeperUserInbox
                        {
                            ShopKeeperUserId = shopKeeper.Result.Id,
                            CreatedAt = DateTime.Now,
                            Message = message
                        };
                        Context.Set<ShopKeeperUserInbox>().Add(shopKeeperInbox);                        
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return message;
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if shop have duplicated size for each product group thrown error.
        /// </summary>
        protected override void ValidateAddOrUpdateRequest(T form)
        {
        }
    }
}