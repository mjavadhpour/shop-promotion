// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.Admin
{
    // API
    using ServiceConfiguration;
    using Infrastructure.Models.Parameter.Custom;
    // Domain
    using Domain.Services.PaginationHelper;
    using Domain.Infrastructure.DAL;
    using Domain.Infrastructure.Models.Response;
    using Domain.Infrastructure.Models.Resource.Custom;

    /// <summary>
    /// Report controller.
    /// </summary>
    [Area("Admin")]
    [Route("api/v1/[area]/Report")]
    [Authorize(Policy = ConfigurePolicyService.AdminUserPolicy)]
    public class AdminReportController : BaseController
    {
        /// <inheritdoc />
        public AdminReportController(ResolvedPaginationValueService defaultPagingOptionsAccessor, UnitOfWork unitOfWork) :
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
        [HttpGet("Payment/Number")]
        [ProducesResponseType(typeof(SingleModelResponse<NumberOfPaymentsReportViewModel>), 200)]
        public async Task<IActionResult> GetNumberOfPaymentsReportAsync(
            [FromQuery] PaymentsReportParameters reportParameters, CancellationToken ct)
        {
            var response =
                new SingleModelResponse<NumberOfPaymentsReportViewModel>() as
                    ISingleModelResponse<NumberOfPaymentsReportViewModel>;
            response.Model = await UnitOfWork.PaymentReportService.GetNumberOfPayments(reportParameters, ct);

            return Ok(response);
        }

        /// <summary>
        /// Get report for the sum of payments.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("Payment/Sum")]
        [ProducesResponseType(typeof(SingleModelResponse<SumOfPaymentsReportViewModel>), 200)]
        public async Task<IActionResult> GetSumOfPaymentsReportAsync(
            [FromQuery] PaymentsReportParameters reportParameters, CancellationToken ct)
        {
            var response =
                new SingleModelResponse<SumOfPaymentsReportViewModel>() as
                    ISingleModelResponse<SumOfPaymentsReportViewModel>;
            response.Model = await UnitOfWork.PaymentReportService.GetSumOfPayments(reportParameters, ct);
            return Ok(response);
        }

        /// <summary>
        /// Get report for the number of shops.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("Shop/Number")]
        [ProducesResponseType(typeof(SingleModelResponse<ShopsReportViewModel>), 200)]
        public async Task<IActionResult> GetShopsReportAsync(
            [FromQuery] ShopsReportParameters reportParameters, CancellationToken ct)
        {
            var response =
                new SingleModelResponse<ShopsReportViewModel>() as
                    ISingleModelResponse<ShopsReportViewModel>;
            response.Model = await UnitOfWork.ShopReportService.GetNumberOfShops(reportParameters, ct);
            return Ok(response);
        }

        /// <summary>
        /// Get report for the number of app users.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("User/Number")]
        [ProducesResponseType(typeof(SingleModelResponse<AppUsersReportViewModel>), 200)]
        public async Task<IActionResult> GetUsersReportAsync(
            [FromQuery] AppUsersReportParameters reportParameters, CancellationToken ct)
        {
            var response =
                new SingleModelResponse<AppUsersReportViewModel>() as
                    ISingleModelResponse<AppUsersReportViewModel>;
            response.Model = await UnitOfWork.UserReportService.GetNumberOfUsers(reportParameters, ct);
            return Ok(response);
        }

        /// <summary>
        /// Get report for the number of app users.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("Usages/Chart")]
        [ProducesResponseType(typeof(SingleModelResponse<IList<UsagesStatisticsViewModel>>), 200)]
        public async Task<IActionResult> GetUsagesStatisticsAsync(
            [FromQuery] UsageStatisticsParameters reportParameters, CancellationToken ct)
        {
            var response =
                new SingleModelResponse<IList<UsagesStatisticsViewModel>>() as
                    ISingleModelResponse<IList<UsagesStatisticsViewModel>>;
            response.Model = await UnitOfWork.UsageStatisticservice.GetUsagesChartReport(reportParameters, ct);
            return Ok(response);
        }
    }
}