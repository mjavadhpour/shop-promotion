// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.Infrastructure.Models.Form.Custom
{
    /// <summary>
    /// Form for upload shop image.
    /// </summary>
    public class Base64ShopImageForm
    {
        /// <summary>
        /// Base 64 string of image.
        /// </summary>
        public string Base64Image { get; set; }

        /// <summary>
        /// The type of uploaded image.
        /// </summary>
        public string ImageType { get; set; }
    }
}