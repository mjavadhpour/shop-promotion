// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource.Custom
{
    using Resource;

    /// <summary>
    /// Reponse model for Report controller.
    /// </summary>
    public class AppUsersReportViewModel
    {
        /// <summary>
        /// List of users.
        /// </summary>
        public IList<MinimumIdentityUserResource> MinimumIdentityUserResources { get; set; }
    }
}