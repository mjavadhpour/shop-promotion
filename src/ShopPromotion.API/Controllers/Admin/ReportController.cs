// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.Admin
{
    // API
    using ServiceConfiguration;
    // Domain
    using Domain.Services.PaginationHelper;
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Parameter.Custom;
    using Domain.Infrastructure.Models.Resource.Custom;

    /// <summary>
    /// Report controller.
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class ReportController : BaseController
    {
        /// <inheritdoc />
        public ReportController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork) :
            base(defaultPagingOptionsAccessor, unitOfWork)
        {
        }

        /// <summary>
        /// Get report for the number of payments.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("payment/number")]
        [ProducesResponseType(typeof(NumberOfPaymentsReportViewModel), 200)]
        public async Task<IActionResult> GetNumberOfPaymentsReportAsync(
            [FromQuery] PaymentsReportParameters reportParameters)
        {
            var result = await UnitOfWork.PaymentReportService.GetNumberOfPayments(reportParameters);
            return Ok(result);
        }

        /// <summary>
        /// Get report for the sum of payments.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("payment/sum")]
        [ProducesResponseType(typeof(SumOfPaymentsReportViewModel), 200)]
        public async Task<IActionResult> GetSumOfPaymentsReportAsync(
            [FromQuery] PaymentsReportParameters reportParameters)
        {
            var result = await UnitOfWork.PaymentReportService.GetSumOfPayments(reportParameters);
            return Ok(result);
        }

        /// <summary>
        /// Get report for the number of shops.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("shop/number")]
        [ProducesResponseType(typeof(ShopsReportViewModel), 200)]
        public async Task<IActionResult> GetShopsReportAsync(
            [FromQuery] ShopsReportParameters reportParameters)
        {
            var result = await UnitOfWork.ShopReportService.GetNumberOfShops(reportParameters);
            return Ok(result);
        }

        /// <summary>
        /// Get report for the number of app users.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("appuser/number")]
        [ProducesResponseType(typeof(AppUsersReportViewModel), 200)]
        public async Task<IActionResult> GetAppUsersReportAsync(
            [FromQuery] AppUsersReportParameters reportParameters)
        {
            var result = await UnitOfWork.AppUserReportService.GetNumberOfAppUsers(reportParameters);
            return Ok(result);
        }

        /// <summary>
        /// Get report for the number of app users.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("usages/chart")]
        [ProducesResponseType(typeof(UsagesStatisticsViewModel), 200)]
        public async Task<IActionResult> GetUsagesStatisticsAsync(
            [FromQuery] UsageStatisticsParameters reportParameters)
        {
            var result = await UnitOfWork.UsageStatisticservice.GetUsagesChartReport(reportParameters);
            return Ok(result);
        }
    }
}