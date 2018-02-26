// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.ServiceConfiguration
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Interface that implemented by all service configuration classes.
    /// </summary>
    public interface IConfigureService
    {
        /// <summary>
        /// Configure service separatly with this method.
        /// </summary>
        /// <param name="services"></param>
        void Configure(IServiceCollection services);
    }
}