// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading.Tasks;
using SmsIrRestful;

namespace ShopPromotion.API.Services
{
    /// <inheritdoc />
    /// IrSmsRestful SMS service.
    public class AuthMessageSender : ISmsSender
    {
        private const string UserApiKey = "886948141256e0835e7f4814";
        private const string SecretKey = "14#cv&0*c$";
        private const string LineNumber = "50002015241270";
        private const string BaseMessage = "کد ورود شما: ";

        private readonly string _token;
        private readonly MessageSend _messageSend;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="messageSend"></param>
        public AuthMessageSender(Token token, MessageSend messageSend)
        {
            _token = token.GetToken(UserApiKey, SecretKey);
            _messageSend = messageSend;
        }

        /// <inheritdoc />
        public Task SendSmsAsync(string number, string message)
        {
            Console.WriteLine(message);
            return Task.FromResult(
                _messageSend
                    .Send(_token, new MessageSendObject
                    {
                        Messages = new [] {$@"{BaseMessage}{message}"},
                        MobileNumbers = new [] {number},
                        LineNumber = LineNumber
                    })
                );
        }
    }
}