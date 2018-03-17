// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The end user or normal customer who is register to the app and want to use the application.
    /// </summary>
    public class AppUser : BaseIdentityUser
    {
        public double TotalPrivilege { get; set; }

        public IList<AppUserInbox> AppUserInboxes { get; set; }

        public IList<AppUserPaymentMethod> AppUserPaymentMethods { get; set; }
    }
}