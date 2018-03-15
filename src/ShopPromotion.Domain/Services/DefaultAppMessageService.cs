// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopPromotion.Domain.Services
{
    using EntityLayer;
    using Infrastructure;
    using Infrastructure.Models.Form;
    using Infrastructure.Models.Resource;
    using Infrastructure.Models.Parameter;
    using PaginationHelper;

    public class
        DefaultAppMessageService<T> : DefaultEntityService<T, MinimumInboxResource, MinimumInboxMessageResource, Message
            , ShopPromotionDomainContext>
        where T : MessageForm
    {
        public DefaultAppMessageService(ShopPromotionDomainContext context,
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

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
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