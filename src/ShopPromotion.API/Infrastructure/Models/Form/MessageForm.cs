// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.ComponentModel.DataAnnotations;

namespace ShopPromotion.API.Infrastructure.Models.Form
{
    using Domain.EntityLayer;

    public class MessageForm : Domain.Infrastructure.Models.Form.MessageForm
    {
        /// <summary>
        /// Target type. Requirenment: <br />
        /// Shop : 0 <br />
        /// AppUser: 1 <br />
        /// ShopKeeper: 2 <br />
        /// All: 3
        /// </summary>
        [Required]
        public override MessageTargetTypeOption MessageTargetType { get; set; }
    }
}