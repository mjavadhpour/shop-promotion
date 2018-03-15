// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The owner of shop who was managed the shop functionality.
    /// </summary>
    public class ShopKeeperUser : BaseIdentityUser
    {
        public IList<ShopKeeperUserInbox> ShopKeeperUserInboxes { get; set; }
    }
}