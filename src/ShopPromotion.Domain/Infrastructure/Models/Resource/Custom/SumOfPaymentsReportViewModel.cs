// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Resource.Custom
{
    /// <summary>
    /// Reponse model for Report controller.
    /// </summary>
    public class SumOfPaymentsReportViewModel
    {
        public SumOfPaymentsReportViewModel(int sum)
        {
            Sum = sum;
        }

        /// <summary>
        /// Sum of payments.
        /// </summary>
        public int Sum { get; }
    }
}