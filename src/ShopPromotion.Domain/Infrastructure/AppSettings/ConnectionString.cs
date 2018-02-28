// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.AppSettings
{
    public class ConnectionString
    {
        /// <summary>
        /// SQLServer connection string is default connection of this system.
        /// </summary>
        public string DefaultConnection { get; set; }

        public string MySqlConnection { get; set; }
    }
}