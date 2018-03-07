// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Resource.Custom
{
    /// <summary>
    /// Reponse model for Report controller.
    /// </summary>
    public class AppUsersReportViewModel
    {
        public AppUsersReportViewModel(int appUserCount, int shopKeeperCount, int adminCount)
        {
            AppUserCount = appUserCount;
            ShopKeeperCount = shopKeeperCount;
            AdminCount = adminCount;
        }

        /// <summary>
        /// The count of app users.
        /// </summary>
        public int AppUserCount { get; }

        /// <summary>
        /// The count of shop keepers.
        /// </summary>
        public int ShopKeeperCount { get; }

        /// <summary>
        /// The count of admins.
        /// </summary>
        public int AdminCount { get; }
    }
}