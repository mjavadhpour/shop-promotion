// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.Infrastructure.Models.Parameter
{
    public class GeolocationPoint
    {
        public double Latitude { get; set;}

        public double Longitude { get; set; }

        /// <summary>
        /// Radius in memters.
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// If this object is empty. all parameters was validated.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return Latitude == 0 || Longitude == 0 || Radius == 0;
        }
    }
}