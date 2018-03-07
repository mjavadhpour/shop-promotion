// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The Status of a Shop. This status will promote with Admin and can be: Approved, Disapproved, Need Change.
    /// <see cref="ShopStatusOption"/>
    /// </summary>
    public class ShopStatus : BaseEntity
    {
        public ShopStatus()
        {
            Status = ShopStatusOption.Disapproved;
        }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public ShopStatusOption Status { get; set; }
    }
}