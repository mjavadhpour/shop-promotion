// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace ShopPromotion.API.Controllers.App
{
    // API
    using ServiceConfiguration;
    using Infrastructure.Models.Parameter;
    // Domain
    using Domain.Services.PaginationHelper;
    using Domain.Infrastructure.DAL;
    using Domain.EntityLayer;
    using Domain.Exceptions;
    using Infrastructure.Models.Form.Custom;
    using Domain.Infrastructure.Models.Resource;
    using Domain.Infrastructure.Models.Response;

    /// <summary>
    /// Shop image controller.
    /// </summary>
    [Area("App")]
    [Route("api/v1/[area]")]
    [Authorize(Policy = ConfigurePolicyService.ShopKeeperUserPolicy)]
    public class ShopImageController : BaseController
    {
        /// <inheritdoc />
        public ShopImageController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork) : base(defaultPagingOptionsAccessor, unitOfWork)
        {
        }

        /// <summary>
        /// Upload an image for shop.
        /// </summary>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("Shop/{ItemId}/image")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumShopImageResource>), 201)]
        public async Task<IActionResult> UploadShopImageAsync(GetItemByIdParameters byIdParameters,
            [FromBody] Base64ShopImageForm shopImageBase64Form,
            CancellationToken ct)
        {
            // Find requested shop
            var shop = UnitOfWork.Context.Shops.AsNoTracking()
                .Where(x => x.Id == byIdParameters.ItemId)
                .Select(x => x.Id)
                .SingleOrDefaultAsync(ct);

            // If shop not found
            if (shop.Result == 0) throw new ShopNotFoundException();

            // UPLOAD IMAGE
            var randomFileName = Path.GetRandomFileName();
            // Path to file in dedicated location
            var filePath =
                Path.Combine(
                    $"{Environment.CurrentDirectory}/wwwroot/img/shop/{randomFileName}.{shopImageBase64Form.ImageType}");
            // Save file to requested directory.
            try
            {
                var base64String = shopImageBase64Form.Base64Image;
                var base64Array = Convert.FromBase64String(base64String);
                System.IO.File.WriteAllBytes(filePath, base64Array);
            }
            catch (FormatException)
            {
                throw new NotValidBase64ShopImageException();
            }

            // Manually map to target object. it's better to handle with automapper. we do it manually
            // because automapper create just once then in CreateAt and Code we don't have unique values.
            var shopImage = new ShopImage
            {
                Path = filePath,
                ShopId = shop.Result,
                Type = shopImageBase64Form.ImageType,
                CreatedAt = DateTime.Now
            };
            UnitOfWork.Context.ShopImages.Add(shopImage);
            await UnitOfWork.SaveAsync();

            // Prepare response.
            var response =
                new SingleModelResponse<MinimumShopImageResource>() as ISingleModelResponse<MinimumShopImageResource>;
            response.Model = Mapper.Map<MinimumShopImageResource>(shopImage);
            return Ok(response);
        }
    }
}