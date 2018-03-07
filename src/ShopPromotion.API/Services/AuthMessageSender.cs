// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.Threading.Tasks;
using LiteDB;
using SmsIrRestful;

namespace ShopPromotion.API.Services
{
    using Domain.EntityLayer.LiteDb;

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

        private readonly LiteDatabase _liteDatabase;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="messageSend"></param>
        /// <param name="lightDb"></param>
        public AuthMessageSender(Token token, MessageSend messageSend, LiteDatabase lightDb)
        {
            _token = token.GetToken(UserApiKey, SecretKey);
            _messageSend = messageSend;
            _liteDatabase = lightDb;
        }

        /// <inheritdoc />
        public Task SendSmsAsync(string number, string message)
        {
            Console.WriteLine(message);
            return Task.FromResult(Send(number, message));
        }

        private bool Send(string number, string message)
        {
            // Send
            var messageResponse = _messageSend
                .Send(_token, new MessageSendObject
                {
                    Messages = new[] {$@"{BaseMessage}{message}"},
                    MobileNumbers = new[] {number},
                    LineNumber = LineNumber
                });

            // Log
            var smsUsageLog = _liteDatabase.GetCollection<SmsUsage>();
            var smsUsage = new SmsUsage
            {
                IsSucceed = messageResponse.IsSuccessful,
                SentAt = DateTime.Now
            };
            // Insert new customer document (Id will be auto-incremented)
            smsUsageLog.Insert(smsUsage);

            return true;
        }
    }
}