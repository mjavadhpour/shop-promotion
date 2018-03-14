// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when can not find requested paymet method.
    /// </summary>
    public class PaymentMethodNotFoundException : Exception
    {
        /// <inheritdoc />
        public PaymentMethodNotFoundException() : base("Can not find requested paymentMethod!")
        {
        }
    }
}