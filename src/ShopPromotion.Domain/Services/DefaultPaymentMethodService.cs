// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

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
        DefaultPaymentMethodService<T> : DefaultEntityService<T, MinimumPaymentMethodResource,
            MinimumPaymentMethodResource, PaymentMethod, ShopPromotionDomainContext>
        where T : PaymentMethodForm
    {
        public DefaultPaymentMethodService(ShopPromotionDomainContext context,
            ResolvedPaginationValueService resolvedPaginationValue) : base(context, resolvedPaginationValue)
        {
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override async Task<PaymentMethod> GetElementOfTModelSequenceAsync(int id,
            CancellationToken ct)
        {
            return await Entities
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id, ct);
        }

        /// <inheritdoc>
        ///     <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override IQueryable<PaymentMethod> GetElementsOfTModelSequenceAsync(
            IEntityTypeParameters entityTypeParameters)
        {
            // Filter by Gateway Config Id.
            if (entityTypeParameters.GetParameter("GatewayConfigId") != null)
                Query = Query.Where(
                    x => x.GatewayConfigId == (int) entityTypeParameters.GetParameter("GatewayConfigId"));

            // Just payment method of current logged in user.
            Query = Query.Where(
                x => x.AppUserPaymentMethod.OwnerId == (string) entityTypeParameters.GetParameter("CurrentUserId"));

            return base.GetElementsOfTModelSequenceAsync(entityTypeParameters);
        }

        /// <inheritdoc>
        /// <cref>DefaultEntityService{TForm, TModelResource,TModel}</cref>
        /// </inheritdoc>
        protected override PaymentMethod MappingFromModelToTModelDestination(T form, CancellationToken ct)
        {
            // Map shop promotion and resource.
            var paymentMethod = Mapper.Map<PaymentMethod>(form);

            // Assign gateway config if exists any.
            if (form.GatewayConfigId != null)
            {
                // Assign gateway to new payment method.
                var gatewayConfig = Context.GatewayConfigs.AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == form.GatewayConfigId, ct);
                // Validation requested id.
                if (gatewayConfig.Result == null) throw new GatewayConfigNotFoundException();
                paymentMethod.GatewayConfigId = gatewayConfig.Result.Id;
            }

            // Assign payment method to logged in user.
            var appUserPaymentMethod = new AppUserPaymentMethod
            {
                OwnerId = form.CreatedById,
                PaymentMethod = paymentMethod 
            };
            Context.AppUserPaymentMethods.Add(appUserPaymentMethod);

            return paymentMethod;
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