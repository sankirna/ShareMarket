using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ShareMarket.BusinessLogic.Helpers;
using ShareMarket.Core.Enums;
using ShareMarket.BusinessLogic.Models;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.BusinessLogic.Libs
{
    public class HomeLib : IDisposable
    {
        #region "Private Member(s)"

        private bool _disposed = false;
        private IComponentContext _context;

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="context">IComponentContext</param>
        public HomeLib(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        /// <summary>
        /// User Login with emailAddress and Password
        /// </summary>
        /// <param name="login">Login Model</param>
        /// <returns>UserInfo Entity</returns>
        public bool Login(LoginModel login)
        {
            try
            {
                using (IWebSecurity webSecurity = _context.Resolve<IWebSecurity>())
                {
                    return webSecurity.Login(login.EmailAddress, login.Password, persistCookie: login.RememberMe);
                }
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }

            return false;
        }


        /// <summary>
        /// Create New User Web Security
        /// </summary>
        /// <param name="userAccount">Registration parameter and company user </param>
        /// <param name="contactId">contact id which user mapped </param>
        /// <param name="role">role under user exits</param>
        /// <returns>true/false</returns>
        public bool CreateUser()
        {
            using (IWebSecurity webSecurity = _context.Resolve<IWebSecurity>())
            {
                // Create user using web security
                string userName = "SankirnaRana" + Guid.NewGuid();
                webSecurity.CreateUserAndAccount(userName, "test#1234", new
                {
                    UserType = UserType.Admin
                }, requireConfirmationToken: true);

                using (IRole _role = _context.Resolve<IRole>())
                {
                    _role.AddUserToRole(userName,RoleType.Admin.ToString());
                    return true;
                }

                // Step 1: Add role in current user
                //MapUserInRole(userAccount.UserName, role);
                return true;
            }
        }


        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose Logic
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // dispose-only, i.e. non-finalizable logic
                    _context = null;
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
