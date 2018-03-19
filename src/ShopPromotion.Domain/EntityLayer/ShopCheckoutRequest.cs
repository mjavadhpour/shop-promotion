// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// A base table for shop keeper checkout request given to admin.
    /// </summary>
    public class ShopCheckoutRequest : BaseEntity
    {
        [NotMapped]
        public override int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CheckoutRequestId { get; set; }

        public string ShopKeeperUserId { get; set; }
        public ShopKeeperUser ShopKeeperUser { get; set; }

        public IList<ShopCheckoutRequestOrder> Orders { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}