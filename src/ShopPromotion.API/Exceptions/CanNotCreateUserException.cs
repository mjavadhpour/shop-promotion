// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when can not create user with <see cref="Microsoft.AspNetCore.Identity.UserManager{T}.CreateAsync(T)"/>
    /// </summary>
    public class CanNotCreateUserException : Exception
    {
        /// <inheritdoc />
        public CanNotCreateUserException() : base("Can not create user!")
        {
        }
    }
}