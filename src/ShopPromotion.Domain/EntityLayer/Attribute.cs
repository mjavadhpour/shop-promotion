// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The available attribute for ShopAttributes.
    /// </summary>
    public class Attribute : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}