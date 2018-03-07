// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;

namespace ShopPromotion.Domain.Infrastructure.Models.Resource.Custom
{
    /// <summary>
    /// Reponse model for Report controller.
    /// </summary>
    public sealed class UsagesStatisticsViewModel
    {
        public UsagesStatisticsViewModel(DateTime date, int user, int shop, int payment, int sms, int scannedBarcode)
        {
            Date = date;
            User = user;
            Shop = shop;
            Payment = payment;
            Sms = sms;
            ScannedBarcode = scannedBarcode;
        }

        public DateTime Date { get; }

        public int User { get; }

        public int Shop { get; }

        public int Payment { get; }

        public int Sms { get; }

        public int ScannedBarcode { get; }
    }
}