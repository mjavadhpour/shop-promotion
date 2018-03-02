// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The Privilege of the app user. This privilege was promoted to user by financial transaction or other things.
    /// </summary>
    /// <remarks>
    /// The reference of Privilege was not here because maybe the privilege updated and we lost current privilege here.
    /// </remarks>
    public class AppUserPrivilege : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string PrivilegeLog { get; set; }

        /// <summary>
        /// The order's log, the transaction's log, and more...
        /// </summary>
        public string Description { get; set; }
    }
}