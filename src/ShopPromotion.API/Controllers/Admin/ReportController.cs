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
    using Infrastructure.Models.Parameter;
    using Infrastructure.Models.ActionResults;
    using Services.Statistics;
    // Domain
    using Domain.Services.PaginationHelper;

    /// <summary>
    /// Report controller.
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class ReportController : BaseController
    {
        private readonly IPaymentReportTracker _paymentReportService;
        private readonly IShopReportService _shopReportService;
        private readonly IAppUserReportService _appUserReportService;
        private readonly IUsageStatisticservice _usageStatisticservice;

        /// <inheritdoc />
        public ReportController(
            ResolvedPaginationValueService defaultPagingOptionsAccessor,
            IPaymentReportTracker paymentReportService,
            IShopReportService shopReportService,
            IUsageStatisticservice usageStatisticservice,
            IAppUserReportService appUserReportService) : base(
            defaultPagingOptionsAccessor)
        {
            _paymentReportService = paymentReportService;
            _appUserReportService = appUserReportService;
            _shopReportService = shopReportService;
            _usageStatisticservice = usageStatisticservice;
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
            var result = await _paymentReportService.GetNumberOfPayments(reportParameters);
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
            var result = await _paymentReportService.GetSumOfPayments(reportParameters);
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
            var result = await _shopReportService.GetNumberOfShops(reportParameters);
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
            var result = await _appUserReportService.GetNumberOfAppUsers(reportParameters);
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
            var result = await _usageStatisticservice.GetUsagesChartReport(reportParameters);
            return Ok(result);
        }
    }
}