﻿// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Infrastructure.Models.Parameter
{
    using Domain.Infrastructure.Models.Parameter;

    public class GetAllShopsParameters : IEntityTypeParameters
    {
        /// <summary>
        /// Filter shop by created time. <br />
        /// Use <b>0</b> for <b>Last hour</b> <br />
        /// Use <b>1</b> for <b>Today</b> <br />
        /// Use <b>2</b> for <b>This week</b> <br />
        /// Use <b>3</b> for <b>This month</b> <br />
        /// Use <b>4</b> for <b>This year</b> <br />
        /// </summary>
        [FromQuery]
        public DateFilterParameterOptions CreateDate { get; set; }

        /// <inheritdoc />
        public object GetParameter(string nameOfParam)
        {
            switch (nameOfParam)
            {
                case "CreateDate":
                    return CreateDate;
                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}