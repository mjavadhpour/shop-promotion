// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Form
{
    public class OrderItemForm : BaseOrderForm
    {
        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}