using System.Linq;
using System.Web.Mvc;
using Autofac;
using ShareMarket.Utility.Utilities;


namespace ShareMarket.Web.Areas.Filters
{
    public class AuthorizedUseAttribute : ActionFilterAttribute
    {
        public bool IsCheck { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (IsCheck)
            {
                // Check User Login for not
                if (user == null || !user.Identity.IsAuthenticated)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    // Get user roles
                    //using (UserAccessLib userAccessLib = GlobalUtil.Container.Resolve<UserAccessLib>())
                    //{
                    //    // Check user login as Proper role or not
                    //    if (!userAccessLib.IsEntityRoleAccess(Entity, RoleAccess))
                    //    {
                    //        filterContext.Result = new RedirectResult("~/Home/Unauthorised");
                    //        return;
                    //    }

                    //}

                }
            }

        }
    }
}