// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    /// <summary>
    /// Controllers parameter class model.
    /// </summary>
    public class GetItemByIdParameters
    {
        /// <summary>
        /// Item id.
        /// </summary>
        [FromRoute]
        [Required]
        public int ItemId { get; set; }
    }

    /// <summary>
    /// SpecialOffer controllers parameter class model.
    /// </summary>
    public class GetItemByIdAndShopParameters : GetItemByIdParameters
    {
        /// <summary>
        /// Shop id.
        /// </summary>
        [FromRoute]
        [Required]
        public int ShopId { get; set; }
    }

    /// <summary>
    /// SpecialOffer controllers parameter class model.
    /// </summary>
    public class GetItemByIdAndShopAndPromotionParameters : GetItemByIdParameters
    {
        /// <summary>
        /// Shop id.
        /// </summary>
        [FromRoute]
        [Required]
        public int ShopId { get; set; }

        /// <summary>
        /// Promotion id.
        /// </summary>
        [FromRoute]
        [Required]
        public int PromotionId { get; set; }
    }

    /// <summary>
    /// Controller parameter for get user by Phone number.
    /// </summary>
    public class GetItemByPhoneNumberParameters
    {
        /// <summary>
        /// PhoneNumber
        /// </summary>
        [FromRoute]
        [Required]
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// Controller parameter for get user by Phone number and id.
    /// </summary>
    public class GetItemByIdAndPhoneNumberParameters : GetItemByIdParameters
    {
        /// <summary>
        /// PhoneNumber
        /// </summary>
        [FromRoute]
        [Required]
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// Controller paramter for get Barcode by Code.
    /// </summary>
    public class GetItemByCodeParameters
    {
        /// <summary>
        /// Code
        /// </summary>
        [FromRoute]
        [Required]
        public long Code { get; set; }
    }
}