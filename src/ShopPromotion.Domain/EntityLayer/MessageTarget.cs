// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Domain.EntityLayer
{
    /// <summary>
    /// The requested target of specific message.
    /// </summary>
    public class MessageTarget : BaseEntity
    {
        public int MessageId { get; set; }
        public Message Message { get; set; }

        public MessageTargetTypeOption TargetType { get; set; }

        public string TargetObjectId { get; set; }
    }
}