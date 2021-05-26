using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Demo.DDD.Security
{
    public class ScopeAuthorizationHandler : AuthorizationHandler<ScopesRequirement>
    {
        readonly IHttpContextAccessor _httpContextAccessor = null;

        public ScopeAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopesRequirement requirement)
        {
            var scopes = context.User.Claims
                .Where(x => x.Type.Equals("scope"))
                .Select(claim => claim.Value)
                .ToArray();

            var requirementSatisfied = requirement.Items.All(requiredScope => scopes.Contains(requiredScope));

            if (requirementSatisfied)
            {
                context.Succeed(requirement);
            }
            else
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = 403;
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}