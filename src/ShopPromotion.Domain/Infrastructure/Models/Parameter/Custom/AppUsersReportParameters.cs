// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Parameter.Custom
{
    /// <summary>
    /// Model for query on appUsers report results.
    /// </summary>
    public class AppUsersReportParameters : BaseReportParameters
    {
        /// <summary>
        /// Filter by username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Filter by ID.
        /// </summary>
        public string Id { get; set; }
    }
}