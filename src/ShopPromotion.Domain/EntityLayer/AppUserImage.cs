// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The images of app user.
    /// </summary>
    public class AppUserImage : BaseEntity
    {
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }

        public string Type { get; set; }

        public string Path { get; set; }
    }
}