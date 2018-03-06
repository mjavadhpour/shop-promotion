// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The options of gateway.
    /// </summary>
    public class GatewayConfig : BaseEntity
    {
        public string GatewayName { get; set; }

        public string Config { get; set; }
    }
}