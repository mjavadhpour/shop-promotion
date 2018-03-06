// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// Payment, in other word financial trancactions.
    /// </summary>
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int PaymentMothodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int Amount { get; set; }

        public string State { get; set; }

        public string Details { get; set; }
    }
}