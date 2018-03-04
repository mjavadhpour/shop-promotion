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
    /// Controller parameter for get user by UserName.
    /// </summary>
    public class GetItemByUserNameParameters
    {
        /// <summary>
        /// UserName
        /// </summary>
        [FromRoute]
        [Required]
        public string UserName { get; set; }
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