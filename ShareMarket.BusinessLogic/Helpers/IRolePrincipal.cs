using System;

namespace ShareMarket.BusinessLogic.Helpers
{
    public interface IRolePrincipal : IDisposable 
    {
        //
        // Summary:
        //     Gets a list of roles that the System.Web.Security.RolePrincipal is a member
        //     of.
        //
        // Returns:
        //     The list of roles that the System.Web.Security.RolePrincipal is a member
        //     of.
        //
        // Exceptions:
        //   System.Configuration.Provider.ProviderException:
        //     The System.Web.Security.RolePrincipal.Identity property is null.
         string[] GetRoles();
        //
        // Summary:
        //     Gets a value indicating whether the user represented by the System.Web.Security.RolePrincipal
        //     is in the specified role.
        //
        // Parameters:
        //   role:
        //     The role to search for.
        //
        // Returns:
        //     true if user represented by the System.Web.Security.RolePrincipal is in the
        //     specified role; otherwise, false.
        //
        // Exceptions:
        //   System.Configuration.Provider.ProviderException:
        //     The System.Web.Security.RolePrincipal.Identity property is null.
        bool IsInRole(string role);
    }
}
