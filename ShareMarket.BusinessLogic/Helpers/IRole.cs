using System;

namespace ShareMarket.BusinessLogic.Helpers
{
    // Summary:
    //     Manages user membership in roles for authorization checking in an ASP.NET
    //     application. This class cannot be inherited.
    public interface IRole : IDisposable
    {
        //
        // Summary:
        //     Adds the specified user to the specified role.
        //
        // Parameters:
        //   username:
        //     The user name to add to the specified role.
        //
        //   roleName:
        //     The role to add the specified user name to.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     roleName is null.-or-username is null.
        //
        //   System.ArgumentException:
        //     roleName is an empty string or contains a comma (,).-or-username is an empty
        //     string or contains a comma (,).
        //
        //   System.Configuration.Provider.ProviderException:
        //     Role management is not enabled. -or-User is already assigned to the specified
        //     role.
        void AddUserToRole(string username, string roleName);
        //
        // Summary:
        //     Adds the specified user to the specified roles.
        //
        // Parameters:
        //   username:
        //     The user name to add to the specified roles.
        //
        //   roleNames:
        //     A string array of roles to add the specified user name to.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     One of the roles in roleNames is null.-or-username is null.
        //
        //   System.ArgumentException:
        //     One of the roles in roleNames is an empty string or contains a comma (,).-or-username
        //     is an empty string or contains a comma (,).-or-roleNames contains a duplicate
        //     element.
        //
        //   System.Configuration.Provider.ProviderException:
        //     Role management is not enabled.
        void AddUserToRoles(string username, string[] roleNames);
        //
        // Summary:
        //     Gets a list of the roles that the currently logged-on user is in.
        //
        // Returns:
        //     A string array containing the names of all the roles that the currently logged-on
        //     user is in.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     There is no current logged-on user.
        //
        //   System.Configuration.Provider.ProviderException:
        //     Role management is not enabled.
        string[] GetRolesForUser(string emailAddress);

        //
        // Summary:
        //     Gets a list of all the roles for the application.
        //
        // Returns:
        //     A string array containing the names of all the roles stored in the data source
        //     for the application.
        //
        // Exceptions:
        //   System.Configuration.Provider.ProviderException:
        //     Role management is not enabled.
        string[] GetAllRoles();

        //
        // Summary:
        //     Removes the specified user from the specified role.
        //
        // Parameters:
        //   username:
        //     The user to remove from the specified role.
        //
        //   roleName:
        //     The role to remove the specified user from.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     roleName is null.-or-username is null.
        //
        //   System.ArgumentException:
        //     roleName is an empty string or contains a comma (,)username is an empty string
        //     or contains a comma (,).
        //
        //   System.Configuration.Provider.ProviderException:
        //     Role management is not enabled.
        void RemoveUserFromRole(string username, string roleName);
    }
}
