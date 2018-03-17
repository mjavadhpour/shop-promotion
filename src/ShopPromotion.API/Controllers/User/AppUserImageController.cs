// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopPromotion.API.Controllers.User
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
    /// AppUser image controller.
    /// </summary>
    [Area("AppUser")]
    [Route("api/v1/[area]/Profile")]
    [Authorize(Policy = ConfigurePolicyService.AppUserPolicy)]
    public class UserImageController : BaseController
    {
        /// <inheritdoc />
        public UserImageController(ResolvedPaginationValueService defaultPagingOptionsAccessor,
            UnitOfWork unitOfWork) : base(defaultPagingOptionsAccessor, unitOfWork)
        {
        }

        /// <summary>
        /// Upload an image for app user.
        /// </summary>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("{PhoneNumber}/image")]
        [ProducesResponseType(typeof(SingleModelResponse<MinimumAppUserImageResource>), 201)]
        public async Task<IActionResult> UploadAppUserImageAsync(GetItemByPhoneNumberParameters byPhoneNumberParameters,
            [FromBody] Base64ImageForm shopImageBase64Form,
            CancellationToken ct)
        {
            // Find requested app user.
            var appUser =
                await UnitOfWork.ShopPromotionUserManager.FindByPhoneAsync(byPhoneNumberParameters.PhoneNumber, ct);

            // If app user not found
            if (appUser == null) throw new UserNotFoundException();

            // UPLOAD IMAGE
            var randomFileName = $@"{Path.GetRandomFileName()}.{shopImageBase64Form.ImageType}";
            // Path to file in dedicated location
            var filePath = Path.Combine(
                $"{Environment.CurrentDirectory}/wwwroot/img/user/{randomFileName}");
            // Save file to requested directory.
            try
            {
                var base64String = shopImageBase64Form.Base64Image;
                var base64Array = Convert.FromBase64String(base64String);
                System.IO.File.WriteAllBytes(filePath, base64Array);
            }
            catch (FormatException)
            {
                throw new NotValidBase64ImageException();
            }

            // Manually map to target object. it's better to handle with automapper. we do it manually
            // because automapper create just once then in CreateAt and Code we don't have unique values.
            var appUserImage = new AppUserImage
            {
                Path = randomFileName,
                OwnerId = appUser.Id,
                Type = shopImageBase64Form.ImageType,
                CreatedAt = DateTime.Now
            };
            UnitOfWork.Context.AppUserImages.Add(appUserImage);
            await UnitOfWork.SaveAsync();

            // Prepare response.
            var response =
                new SingleModelResponse<MinimumAppUserImageResource>() as
                    ISingleModelResponse<MinimumAppUserImageResource>;
            response.Model = Mapper.Map<MinimumAppUserImageResource>(appUserImage);
            return Ok(response);
        }

        /// <summary>
        /// Stream app user image.
        /// </summary>
        /// <param name="fileNameParameters"></param>
        /// <returns></returns>
        [HttpGet("image/{FileName}")]
        public async Task<IActionResult> GetAppUserImageAsync(GetItemByFileNameParameters fileNameParameters)
        {
            // Path to file in dedicated location
            var filePath =
                Path.Combine(
                    $"{Environment.CurrentDirectory}/wwwroot/img/user/{fileNameParameters.FileName}");
            var image = await Task.FromResult(System.IO.File.OpenRead(filePath));
            return File(image, "image/jpeg");
        }
    }
}