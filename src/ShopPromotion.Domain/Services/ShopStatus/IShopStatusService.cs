// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;

namespace ShopPromotion.Domain.Services.ShopStatus
{   
    using Infrastructure.Models.Form.Custom;

    /// <summary>
    /// AppUser Report service.
    /// </summary>
    public interface IShopStatusService
    {
        /// <summary>
        /// Change shop status.
        /// </summary>
        /// <param name="shopStatusForm"></param>
        /// <returns></returns>
        Task ChangeShopStatus(ShopStatusForm shopStatusForm);
    }
}