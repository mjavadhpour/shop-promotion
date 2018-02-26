// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Helper.Infrastructure.Filters.Swagger
{
    using System.Collections.Generic;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Filter for lowercase all auto generated route by swagger.
    /// </summary>
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            // Get all swagger path.
            var paths = swaggerDoc.Paths;

            //	generate the new keys
            var newPaths = new Dictionary<string, PathItem>();
            var removeKeys = new List<string>();
            foreach (var path in paths)
            {
                var newKey = path.Key.ToLower();
                if (newKey == path.Key) continue;
                removeKeys.Add(path.Key);
                newPaths.Add(newKey, path.Value);
            }

            //	add the new keys
            foreach (var path in newPaths)
                swaggerDoc.Paths.Add(path.Key, path.Value);

            //	remove the old keys
            foreach (var key in removeKeys)
                swaggerDoc.Paths.Remove(key);
        }
    }
}