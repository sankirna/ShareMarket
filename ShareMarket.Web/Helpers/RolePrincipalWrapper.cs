using System;
using System.Web;
using System.Web.Security;
using Autofac;
using ShareMarket.BusinessLogic.Helpers;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.Web.Helpers
{
    public class RolePrincipalWrapper : IRolePrincipal
    {

        #region "Private Member(s)"

        private IComponentContext _context;

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="context">IComponentContext</param>
        public RolePrincipalWrapper(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        public string[] GetRoles()
        {
            try
            {
                RolePrincipal rolePrincipal = (RolePrincipal)HttpContext.Current.User;
                return rolePrincipal.GetRoles();
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }

        }

        public bool IsInRole(string role)
        {
            try
            {
                return HttpContext.Current.User.IsInRole(role);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }

        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}