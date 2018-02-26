// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Helper.Infrastructure.Middleware
{
    using System.Collections.Generic;

    /// <summary>
    /// Simple class containing a list of the headers to add and remove.
    /// </summary>
    public class HeadersPolicy
    {
        /// <summary>
        /// </summary>
        public IDictionary<string, string> SetHeaders { get; }
            = new Dictionary<string, string>();

        /// <summary>
        /// </summary>
        public ISet<string> RemoveHeaders { get; }
            = new HashSet<string>();
    }
}