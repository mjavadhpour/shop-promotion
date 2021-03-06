// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when try to create more than one location for shop.
    /// </summary>
    public class DuplicateShopGeolocationException : Exception
    {
        /// <inheritdoc />
        public DuplicateShopGeolocationException() : base("Each shop one location!")
        {
        }
    }
}