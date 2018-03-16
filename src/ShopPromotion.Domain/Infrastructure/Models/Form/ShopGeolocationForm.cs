// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ShopPromotion.Domain.Infrastructure.Models.Form
{
    using EntityLayer;

    public class ShopGeolocationForm : BaseEntity
    {
        [Required]
        [JsonIgnore]
        public int ShopId { get; set; }

        [Required]
        public string latitude { get; set; }

        [Required]
        public string Longitude { get; set; }
    }
}