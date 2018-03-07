// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Resource.Custom
{
    /// <summary>
    /// Reponse model for Report controller.
    /// </summary>
    public class NumberOfPaymentsReportViewModel
    {
        public NumberOfPaymentsReportViewModel(int count)
        {
            Count = count;
        }

        /// <summary>
        /// The count of payments.
        /// </summary>
        public int Count { get; }
    }
}