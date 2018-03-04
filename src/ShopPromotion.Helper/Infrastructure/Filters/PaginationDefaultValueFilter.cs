// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ShopPromotion.Helper.Infrastructure.Filters
{
    // Domain
    using Domain.ModelLayer.Response.Pagination;
    using Domain.Services.PaginationHelper;

    /// <summary>
    /// There are a couple of ways to implement exception handling globally.
    /// </summary>
    /// <remarks>
    /// <a href="https://github.com/aspnet/Mvc/issues/5260">
    ///     Recommended way to read the Request.Body in an ActionFilter
    /// </a>
    /// <a href="https://www.devtrends.co.uk/blog/dependency-injection-in-action-filters-in-asp.net-core">
    ///     Dependency Injection in action filters in ASP.NET Core
    /// </a>
    /// </remarks>
    public class PaginationDefaultValueFilter : ActionFilterAttribute
    {
        private readonly ResolvedPaginationValue _resolvedPaginationValue;
        private readonly IConfiguration _appSettings;

        public PaginationDefaultValueFilter(ResolvedPaginationValue resolvedPaginationValue)
        {
            _resolvedPaginationValue = resolvedPaginationValue;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _appSettings = builder.Build();
        }

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("pagingOptions"))
            {
                // Requested value.
                var requestedPagingOptions =
                    context.ActionArguments["pagingOptions"] as PagingOptions ?? new PagingOptions();

                int? page = int.Parse(_appSettings["AppSettings:PagingOptions:page"]);
                int? pageSize = int.Parse(_appSettings["AppSettings:PagingOptions:pageSize"]);
                var orderBy = _appSettings["AppSettings:PagingOptions:orderBy"];
                var ascendig = _appSettings["AppSettings:PagingOptions:ascending"];

                var tempPage = requestedPagingOptions.Page ?? page ?? 0;
                _resolvedPaginationValue.Page = (int) (tempPage != 0 ? tempPage - 1 : 0);

                _resolvedPaginationValue.PageSize = (int) (requestedPagingOptions.PageSize ??
                                                           pageSize??
                                                           PagingOptions.MaxItemPerPage);

                _resolvedPaginationValue.OrderBy =
                    requestedPagingOptions.OrderBy ?? orderBy ?? "id";

                var tempAscending = requestedPagingOptions.Ascending ?? ascendig ?? "ASC";
                _resolvedPaginationValue.Ascending = tempAscending == "ASC";
            }
        }
    }
}