using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ShareMarket.BusinessLogic.Helpers;
using ShareMarket.Core;
using ShareMarket.Core.Enums;
using ShareMarket.DataAccess.Repository;
using ShareMarket.Utility.Enum;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.BusinessLogic.Libs
{
    public class UserLib : IDisposable
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
        public UserLib(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        /// <summary>
        /// Check user exists or not by email address
        /// </summary>
        /// <param name="userName">Email Address</param>
        /// <returns>true/false</returns>
        public bool IsUserExists(string userName)
        {
            try
            {
                using (IDataRepository<UserProfile> userProfileContext = _context.Resolve<IDataRepository<UserProfile>>())
                {
                    return userProfileContext.Any(u => u.UserName == userName && !u.IsDeleted);
                }
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
            return false;
        }

        /// <summary>
        /// Check user exists or not by email address with edit time
        /// </summary>
        /// <param name="userName">Email Address</param>
        ///  /// <param name="userId">client User Id </param>
        /// <returns>true/false</returns>
        public bool IsEditUserExists(string userName, int userId)
        {
            try
            {
                using (IDataRepository<UserProfile> userProfileContext = _context.Resolve<IDataRepository<UserProfile>>())
                {
                    return userProfileContext.Any(u => u.Id != userId && u.UserName == userName && !u.IsDeleted);
                }
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
            return false;
        }

        /// <summary>
        /// Map user role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role">role under user exits</param>
        /// <returns></returns>
        public bool MapUserInRole(string userName, RoleType role)
        {
            try
            {
                // Step 2: Add role in current user
                using (IRole _role = _context.Resolve<IRole>())
                {
                    _role.AddUserToRole(userName, role.ToString().Replace('_', ' '));
                    return true;
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
        /// <param name="password"></param>
        /// <param name="role">role under user exits</param>
        /// <param name="userProfile"></param>
        /// <param name="userType"></param>
        /// <returns>true/false</returns>
        public bool CreateUser(UserProfile userProfile, string password, RoleType role, UserType userType)
        {
            using (IWebSecurity webSecurity = _context.Resolve<IWebSecurity>())
            {
                // Create user using web security
                webSecurity.CreateUserAndAccount(userProfile.UserName, password, new
                {
                    IsDeleted=false,
                    UserType=userType,
                    CreatedOn = DateUtil.GetCurrentDateTime()
                }, false);

                // Step 1: Add role in current user
                MapUserInRole(userProfile.UserName, role);
                return true;
            }
        }

        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="newPassword">Password</param>
        /// <returns>true/false</returns>
        public bool UpdatePassword(string userName, string newPassword)
        {
            using (IWebSecurity webSecurity = _context.Resolve<IWebSecurity>())
            {
                return webSecurity.ResetPassword(webSecurity.GeneratePasswordResetToken(userName), newPassword);
            }
        }


        /// <summary>
        /// Delete Company
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            try
            {   // Check project is not null
                using (IDataRepository<UserProfile> userProfileContext = _context.Resolve<IDataRepository<UserProfile>>())
                {
                    //UpdatePassword

                    UserProfile userProfile = _context.Resolve<IDataRepository<UserProfile>>().FirstOrDefault(u => (!u.IsDeleted) && u.Id == userId);
                    if (userProfile != null)
                    {
                        UpdatePassword(userProfile.UserName, Guid.NewGuid().ToString());
                    }
                    userProfileContext.Delete(userId);
                    userProfileContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }

            return false;
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
