// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    /// <summary>
    /// Used in GetAll...Parameters when want to apply CreateDate filter.
    /// </summary>
    public abstract class FilterByCreateDate
    {
        /// <summary>
        /// Filter shop by created time. <br />
        /// Use <b>0</b> for <b>Last hour</b> <br />
        /// Use <b>1</b> for <b>Today</b> <br />
        /// Use <b>2</b> for <b>This week</b> <br />
        /// Use <b>3</b> for <b>This month</b> <br />
        /// Use <b>4</b> for <b>This year</b> <br />
        /// </summary>
        [FromQuery]
        public DateFilterParameterOptions? CreateDate { get; set; }
    }
}