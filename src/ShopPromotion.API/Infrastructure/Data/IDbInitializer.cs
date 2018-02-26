// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.Data
{
    /// <summary>
    /// Database initializer in runtime.
    /// </summary>
    public interface IDbInitializer
    {
        /// <summary>
        /// Initialize data for database in runtime.
        /// </summary>
        void Initialize();
    }
}