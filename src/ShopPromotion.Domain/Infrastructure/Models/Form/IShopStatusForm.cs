// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Form
{
    using EntityLayer;

    public interface IShopStatusForm
    {
        /// <summary>
        /// ID of shop.
        /// </summary>
        int ShopId { get; set; }

        /// <summary>
        /// Target status. <br /> Approved: 0 <br /> Disapproved: 1 <br /> NeedChange: 2
        /// </summary>
        ShopStatusOption StatusOptions { get; set; }
    }
}