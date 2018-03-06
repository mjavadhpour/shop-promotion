// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// Payment method, have and options about gateway and more.
    /// </summary>
    public class PaymentMethod : BaseEntity
    {
        public int GatewayConfigId { get; set; }
        public GatewayConfig GatewayConfig { get; set; }

        public string Code { get; set; }

        public bool IsEnabled { get; set; }

        public int Position { get; set; }
    }
}