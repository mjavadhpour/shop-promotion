// Copyright (C) 2018 Mohammad Javad HoseinPour. All rights reserved.
// Licensed under the Private License. See LICENSE in the project root for license information.
// Author: Mohammad Javad HoseinPour <mjavadhpour@gmail.com>

namespace ShopPromotion.API.ServiceConfiguration
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configure identity services.
    /// </summary>
    public class ConfigurePolicyService : IConfigureService
    {
        /// <summary>
        /// The claim type value, in other word the claim value was assigned to this key.
        /// </summary>
        public const string ClaimType = "Role";

        /// <summary>
        /// The Admin claim value. Used in <see cref="AdminUserPolicy"/> and <see cref="ShopKeeperUserPolicy"/> and
        /// <see cref="AppUserPolicy"/>.
        /// </summary>
        public const string AdminUserClaimVelue = "AdminUserClaim";

        /// <summary>
        /// The owner and manager of a shop claim value. Used in <see cref="ShopKeeperUserPolicy"/> and
        /// <see cref="AppUserPolicy"/>.
        /// </summary>
        public const string ShopKeeperUserClaimVelue = "ShopKeeperClaim";

        /// <summary>
        /// The Client claim value. Used in <see cref="AppUserPolicy"/>.
        /// </summary>
        public const string AppUserClaimVelue = "AppUserClaim";

        /// <summary>
        /// Administrator of application. allow to access to anythings.
        /// </summary>
        public const string AdminUserPolicy = "AdminUserPolicy";

        /// <summary>
        /// The owner and manager of a shop policy. allow to acces to all shop functionallity.
        /// </summary>
        public const string ShopKeeperUserPolicy = "ShopKeeperUserPolicy";

        /// <summary>
        /// Customer or end user who was registered to site policy. allow to use this application for their needs.
        /// It was difference with `ShopCustomer` who was not registered to Application and just want to buy something.
        /// </summary>
        public const string AppUserPolicy = "AppUserPolicy";

        /// <inheritdoc />
        void IConfigureService.Configure(IServiceCollection services)
        {
            Configure(services);
        }

        /// <summary>
        /// Real method for configuration.
        /// </summary>
        /// <param name="services"></param>
        public static void Configure(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AdminUserPolicy,
                    policy => policy.RequireClaim(ClaimType, AdminUserClaimVelue));
                options.AddPolicy(ShopKeeperUserPolicy,
                    policy => policy.RequireClaim(ClaimType, AdminUserClaimVelue, ShopKeeperUserClaimVelue));
                options.AddPolicy(AppUserPolicy,
                    policy => policy.RequireClaim(ClaimType, AdminUserClaimVelue, ShopKeeperUserClaimVelue,
                        AppUserClaimVelue));
            });
        }
    }
}