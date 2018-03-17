// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when base 64 image have not valid format.
    /// </summary>
    public class NotValidBase64ImageException : Exception
    {
        /// <inheritdoc />
        public NotValidBase64ImageException() : base("Get an invalid base 64 image!")
        {
        }
    }
}