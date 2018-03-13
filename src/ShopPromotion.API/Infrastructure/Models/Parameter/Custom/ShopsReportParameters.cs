// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.Models.Parameter.Custom
{
    /// <summary>
    /// Model for query on shops report results.
    /// </summary>
    public class ShopsReportParameters : BaseReportParameters
    {
        /// <summary>
        /// Filter by shop's title.
        /// </summary>
        public string Title { get; set; }

        /// <inheritdoc />
        public override object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "Title":
                    return Title;
            }

            return base.GetParameter(nameOfParam);
        }
    }
}