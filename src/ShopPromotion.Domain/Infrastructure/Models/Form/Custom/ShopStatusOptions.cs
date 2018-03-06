// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Form.Custom
{
    /// <summary>
    /// Available status for shop. Approved: 0, Disapproved: 1, NeedChange: 2
    /// </summary>
    public enum ShopStatusOptions
    {
        /// <summary>
        /// Approved.
        /// </summary>
        Approved,
        /// <summary>
        /// Disapproved.
        /// </summary>
        Disapproved,
        /// <summary>
        /// NeedChange.
        /// </summary>
        NeedChange
    }
}