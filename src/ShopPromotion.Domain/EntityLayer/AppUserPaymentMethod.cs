// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The associated payment methods or given app user.
    /// </summary>
    public class AppUserPaymentMethod : BaseEntity
    {
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}