// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Linq;

namespace ShopPromotion.API.Extentions
{
    /// <summary>
    /// Helper class for random functionallity.
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// Generate unique 6 digit number.
        /// </summary>
        /// <returns></returns>
        public static string GenerateNewUniqueRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewUniqueRandom();
            }
            return r;
        }
    }
}