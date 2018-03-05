// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    /// <summary>
    /// Form for change shop status.
    /// </summary>
    public class ShopStatusForm
    {
        /// <summary>
        /// ID of shop.
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// Target status. <br /> Approved: 0 <br /> Disapproved: 1 <br /> NeedChange: 2
        /// </summary>
        public ShopStatusOptions StatusOptions { get; set; }
    }
}