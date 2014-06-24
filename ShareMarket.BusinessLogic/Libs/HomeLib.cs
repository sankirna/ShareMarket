using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ShareMarket.BusinessLogic.Helpers;

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
                string userName = "sankirnaRana" + Guid.NewGuid();
                webSecurity.CreateUserAndAccount(userName, "test#1234");

                using (IRole _role = _context.Resolve<IRole>())
                {
                    _role.AddUserToRole(userName, "Admin");
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
