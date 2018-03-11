// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.Domain.Infrastructure.Models.Parameter
{
    public class GetAllShopPromotionBarcodeParameters : IEntityTypeParameters
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

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "ShopId":
                    return ShopId;
                case "PromotionId":
                    return PromotionId;   
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}