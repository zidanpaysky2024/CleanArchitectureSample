﻿using Architecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Architecture.Infrastructure.Identity
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {

        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            return policy is not null
                ? policy
                : new AuthorizationPolicyBuilder().AddRequirements(new PermissionRequirement(policyName))
                                                  .Build();
        }
    }
}
