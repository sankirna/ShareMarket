using System;
using System.Security.Principal;

namespace ShareMarket.BusinessLogic.Helpers
{
    public interface IWebSecurity : IDisposable 
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        IPrincipal CurrentUser { get; }

        //
        // Summary:
        //     Returns a value that indicates whether the specified user exists in the membership
        //     database.
        //
        // Parameters:
        //   userName:
        //     The user name.
        //
        // Returns:
        //     true if the userName exists in the user profile table; otherwise, false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        bool UserExists(string userName);

        //
        // Summary:
        //     Creates a new user profile entry and a new membership account.
        //
        // Parameters:
        //   userName:
        //     The user name.
        //
        //   password:
        //     The password for the user.
        //
        //   propertyValues:
        //     (Optional) A dictionary that contains additional user attributes. The default
        //     is null.
        //
        //   requireConfirmationToken:
        //     (Optional) true to specify that the user account must be confirmed; otherwise,
        //     false. The default is false.
        //
        // Returns:
        //     A token that can be sent to the user to confirm the user account.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false);

        //
        // Summary:
        //     Returns a value that indicates whether the user has been confirmed.
        //
        // Parameters:
        //   userName:
        //     The user name.
        //
        // Returns:
        //     true if the user is confirmed; otherwise, false.
        bool IsConfirmed(string userName);

        //
        // Summary:
        //     Confirms that an account for the specified user name is valid and activates
        //     the account.
        //
        // Parameters:
        //   userName:
        //     The user name.
        //
        //   accountConfirmationToken:
        //     A confirmation token to pass to the authentication provider.
        //
        // Returns:
        //     true if the account is confirmed; otherwise, false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        bool ConfirmAccount(string userName, string accountConfirmationToken);

        //
        // Summary:
        //     Logs the user in.
        //
        // Parameters:
        //   userName:
        //     The user name.
        //
        //   password:
        //     The password.
        //
        //   persistCookie:
        //     (Optional) true to specify that the authentication token in the cookie should
        //     be persisted beyond the current session; otherwise false. The default is
        //     false.
        //
        // Returns:
        //     true if the user was logged in; otherwise, false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        bool Login(string userName, string password, bool persistCookie = false);

        //
        // Summary:
        //     Logs the user out.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        void Logout();

        //
        // Summary:
        //     Changes the password for the specified user.
        //
        // Parameters:
        //   userName:
        //     The userName.
        //
        //   currentPassword:
        //     The current password for the user.
        //
        //   newPassword:
        //     The new password.
        //
        // Returns:
        //     true if the password is successfully changed; otherwise, false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        bool ChangePassword(string userName, string currentPassword, string newPassword);

        //
        // Summary:
        //     Generates a password reset token that can be sent to a user in userName.
        //
        // Parameters:
        //   userName:
        //     The user name.
        //
        //   tokenExpirationInMinutesFromNow:
        //     (Optional) The time in minutes until the password reset token expires. The
        //     default is 1440 (24 hours).
        //
        // Returns:
        //     A token to send to the user.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow = 1440);

        //
        // Summary:
        //     Resets a password by using a password reset token.
        //
        // Parameters:
        //   passwordResetToken:
        //     A password reset token.
        //
        //   newPassword:
        //     The new password.
        //
        // Returns:
        //     true if the password was changed; otherwise, false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        bool ResetPassword(string passwordResetToken, string newPassword);

        //
        // Summary:
        //     Returns the ID for a user based on the specified user name.
        //
        // Parameters:
        //   userName:
        //     The user name.
        //
        // Returns:
        //     The user ID.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The WebMatrix.WebData.SimpleMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)
        //     method was not called.-or-The Overload:WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
        //     method was not called.-or-The WebMatrix.WebData.SimpleMembershipProvider
        //     membership provider is not registered in the configuration of your site.
        //     For more information, contact your site's system administrator.
        int GetUserId(string userName);

        /// <summary>
        /// Check whether current user is authenticated or not.
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();

        /// <summary>
        /// Gets the current UserId
        /// </summary>
        int CurrentUserId { get; }
    }
}
