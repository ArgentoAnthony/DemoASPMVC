using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPMVC.Tools
{
    public class AdminAuthorizeAttribute : TypeFilterAttribute
    {
        public AdminAuthorizeAttribute() : base(typeof(AdminRequiredFilter))
        {
        }
    }

    public class AdminRequiredFilter : IAuthorizationFilter
    {
        private readonly SessionManager _session;
        public AdminRequiredFilter(SessionManager session)
        {
            _session = session;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_session.ConnectedUser.RoleName != "Admin")
            {
                context.Result = new RedirectToRouteResult(new { action = "NotFound", Controller = "Home" });
            }
        }
    }
}
