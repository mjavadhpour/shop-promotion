// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Newtonsoft.Json;

namespace ShopPromotion.Domain.Infrastructure.Models.Form
{
    public class OrderForm : BaseOrderForm
    {
        [JsonIgnore]
        public string CustomerId { get; set; }

        public string PromotionBarcode { get; set; }

        public string Notes { get; set; }
    }
}