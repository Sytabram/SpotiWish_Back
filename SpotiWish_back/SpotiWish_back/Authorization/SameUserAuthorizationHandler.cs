using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SpotiWish_back.Model;

namespace SpotiWish_back.Authorization
{
    public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement, int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SameUserRequirement requirement,
            int resource)
        {
            var userId = context.User.FindFirst("Id");
            if (userId.Value == resource.ToString() || context.User.IsInRole("admin"))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class SameUserRequirement : IAuthorizationRequirement { }
}