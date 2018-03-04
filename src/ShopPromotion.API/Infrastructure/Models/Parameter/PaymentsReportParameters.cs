// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    /// <summary>
    /// Model for query on payments report results.
    /// </summary>
    public class PaymentsReportParameters : BaseReportParameters
    {
        /// <summary>
        /// Filter result by price. 
        /// </summary>
        public int FromPrice;

        /// <summary>
        /// Filter result by price.
        /// </summary>
        public int ToPrice;

        /// <summary>
        /// Filter by payment method.
        /// </summary>
        public int PaymentMethodId;
    }
}