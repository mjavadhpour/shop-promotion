// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;

namespace ShopPromotion.API.Services
{
    /// <summary>
    /// This class is used by the application to send Email and SMS
    /// when you turn on two-factor authentication in ASP.NET Identity.
    /// For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// Send SMS.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendSmsAsync(string number, string message);
    }
}