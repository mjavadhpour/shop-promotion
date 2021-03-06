// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Collections.Generic;

namespace ShopPromotion.API.Infrastructure.Models.Parameter.Custom
{
    using Domain.Infrastructure.Models.Parameter;

    /// <summary>
    /// Base model for query on report results.
    /// </summary>
    public class BaseReportParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Filter result with Date.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Filter result with Date.
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <inheritdoc />
        public virtual object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "FromDate":
                    return FromDate;
                case "ToDate":
                    return ToDate;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}