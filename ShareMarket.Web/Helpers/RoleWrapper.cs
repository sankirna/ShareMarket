using System;
using System.Web.Security;
using Autofac;
using ShareMarket.BusinessLogic.Helpers;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.Web.Helpers
{
    public class RoleWrapper : IRole
    {
        #region "Private Member(s)"

        private IComponentContext _context;

        #endregion

        #region "Constructor(s)"

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="context">IComponentContext</param>
        public RoleWrapper(IComponentContext context)
        {
            _context = context;
        }

        #endregion

        #region "Public Member(s)"

        /// <summary>
        /// Add single role with user
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name </param>
        public void AddUserToRole(string userName, string roleName)
        {
            try
            {
                Roles.AddUserToRole(userName, roleName);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }

        }

        /// <summary>
        /// Add multiple role with user
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="roleNames">Role Names </param>
        public void AddUserToRoles(string userName, string[] roleNames)
        {
            try
            {
                Roles.AddUserToRoles(userName, roleNames);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
        }

        /// <summary>
        /// Get Role by email id
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns>List Role Names</returns>
        public string[] GetRolesForUser(string userName)
        {
            try
            {
                return Roles.GetRolesForUser(userName);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
            return null;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>List Role Names</returns>
        public string[] GetAllRoles()
        {
            try
            {
                return Roles.GetAllRoles();
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
            return null;
        }

        /// <summary>
        /// Remove user from role
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="roleName">Role Name</param>
        public void RemoveUserFromRole(string userName, string roleName)
        {
            try
            {
                Roles.RemoveUserFromRole(userName, roleName);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
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