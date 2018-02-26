// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.Helper.Infrastructure.ActionResults
{
    using System.ComponentModel;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;

    /// <summary>
    /// Unified model for error in hole of application.
    /// </summary>
    public sealed class ApiError
    {
        /// <summary>
        /// </summary>
        public const string ModelBindingErrorMessage = "Invalid parameters.";

        /// <summary>
        /// </summary>
        public ApiError()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public ApiError(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Creates a new <see cref="ApiError" /> from the result of a model binding attempt.
        /// The first model binding error (if any) is placed in the <see cref="Detail" /> property.
        /// </summary>
        /// <param name="modelState"></param>
        public ApiError(ModelStateDictionary modelState)
        {
            Message = ModelBindingErrorMessage;

            // You could return all of the errors to the user, or traverse the dictionary to pull out the first error:
            // var firstErrorIfAny = modelState
            //     .FirstOrDefault(x => x.Value.Errors.Any())
            //     .Value?.Errors?.FirstOrDefault()?.ErrorMessage;

            Detail = modelState
                .FirstOrDefault(x => x.Value.Errors.Any())
                .Value?.Errors?.FirstOrDefault()?.ErrorMessage;
        }

        /// <summary>
        /// </summary>
        [TempData]
        public string Message { get; set; }

        /// <summary>
        /// </summary>
        [TempData]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        /// <summary>
        /// </summary>
        [TempData]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string StackTrace { get; set; }

        [TempData]
        public int StatusCode { get; set; }
    }
}