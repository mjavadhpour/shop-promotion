// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The items that was into the order (shopping cart). this items is just an price that was entered by AppUser
    /// to apply promotion in the related order and was not a real product.
    /// </summary>
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int UnitPrice { get; set; }

        public int UnitsTotal { get; set; }

        public int Quantity { get; set; }

        public int Total { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}