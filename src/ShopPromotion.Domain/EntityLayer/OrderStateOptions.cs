// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// Order has also its own state, which can have the following values.
    /// </summary>
    public enum OrderStateOptions
    {
        /// <summary>
        /// Before the checkout is completed, it is the initial state of an Order.
        /// </summary>
        Cart,

        /// <summary>
        /// When checkout is completed the cart is transformed into a new order.
        /// </summary>
        New,

        /// <summary>
        /// When the order payments and shipments are completed.
        /// </summary>
        Fulfilled,

        /// <summary>
        /// When the order was cancelled.
        /// </summary>
        Cancelled
    }
}