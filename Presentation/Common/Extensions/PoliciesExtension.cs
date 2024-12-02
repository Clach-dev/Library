using Domain.Constants;
using Domain.Enums;

namespace Presentation.Common.Extensions;

public static class PoliciesExtension
{
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.OnlyAdminAccess, policy =>
                policy.RequireRole(Roles.Admin.ToString()));
            
            options.AddPolicy(Policies.OnlyUserAccess, policy =>
                policy.RequireRole(Roles.User.ToString()));
            
            options.AddPolicy(Policies.AuthenticateAccess, policy =>
                policy.RequireAssertion(_ => true));
        });

        return services;
    }
}