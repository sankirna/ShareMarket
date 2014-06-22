using System;
using System.Security.Principal;
using System.Web;
using Autofac;
using ShareMarket.BusinessLogic.Helpers;
using ShareMarket.Utility.Constants;
using ShareMarket.Utility.Utilities;
using WebMatrix.WebData;

namespace ShareMarket.Web.Helpers
{
    public class WebSecurityWrapper : IWebSecurity
    {
        #region "Private Member(s)"

        private IComponentContext _componentContext;

        #endregion

        /// <summary>
        /// Public Constructor 
        /// </summary>
        /// <param name="componentContext"></param>
        public WebSecurityWrapper(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        #region "Public Properties"

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public IPrincipal CurrentUser
        {
            get { return HttpContext.Current.User; }
        }

        /// <summary>
        ///  Gets the current user id.
        /// </summary>
        public int CurrentUserId
        {
            get
            {
                int userId = WebUtil.GetSessionValue<int>(SessionConstants.UserInfo);
                if (userId <= 0)
                {
                    int currentUserId = WebSecurity.CurrentUserId;
                    if (currentUserId <= 0 && !string.IsNullOrWhiteSpace(CurrentUser.Identity.Name))
                    {
                        //Get user from DB and set CurrentUserID
                    }

                    WebUtil.SetSessionValue(SessionConstants.UserInfo, currentUserId);
                    return currentUserId;
                }
                return userId;
            }
        }
        #endregion

        #region "Public Method(s)"

        /// <summary>
        /// Check whether user exits or not.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UserExists(string userName)
        {
            try
            {
                return WebSecurity.UserExists(userName);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }
            finally
            {

            }

        }

        /// <summary>
        /// Creates the new user account
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="propertyValues"></param>
        /// <param name="requireConfirmationToken"></param>
        /// <returns></returns>
        public string CreateUserAndAccount(string userName, string password, object propertyValues = null,
                                           bool requireConfirmationToken = false)
        {
            try
            {
                return WebSecurity.CreateUserAndAccount(userName, password, propertyValues, requireConfirmationToken);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Check whether user has been confirmed or not.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsConfirmed(string userName)
        {
            try
            {
                return WebSecurity.IsConfirmed(userName);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Check whether user is valid and activated his/her account.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="accountConfirmationToken"></param>
        /// <returns></returns>
        public bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            try
            {
                return WebSecurity.ConfirmAccount(userName, accountConfirmationToken);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Logs the user in.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="persistCookie"></param>
        /// <returns></returns>
        public bool Login(string userName, string password, bool persistCookie = false)
        {
            try
            {
                return WebSecurity.Login(userName, password, persistCookie);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Logs the user out.
        /// </summary>
        public void Logout()
        {
            try
            {
                WebSecurity.Logout();
                WebUtil.AbandonSession();
            }
            catch (Exception ex)
            {
                ex.LogError(this);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Change the password of the specific user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            try
            {
                return WebSecurity.ChangePassword(userName, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Generates the password reset token.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="tokenExpirationInMinutesFromNow"></param>
        /// <returns></returns>
        public string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow = 1440)
        {
            try
            {
                return WebSecurity.GeneratePasswordResetToken(userName, tokenExpirationInMinutesFromNow);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return null;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Reset a password using password reset token.
        /// </summary>
        /// <param name="passwordResetToken"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ResetPassword(string passwordResetToken, string newPassword)
        {
            try
            {

                return WebSecurity.ResetPassword(passwordResetToken, newPassword);
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Get User Id
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns>User Id</returns>
        public int GetUserId(string userName)
        {
            try
            {
                return WebSecurity.GetUserId(userName);
            }
            catch (Exception ex)
            {
                ex.LogError(this);

            }
            finally
            {

            }
            return 0;
        }

        /// <summary>
        /// Check whether user is authenticated or not.
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            try
            {
                return WebSecurity.IsAuthenticated;
            }
            catch (Exception ex)
            {
                ex.LogError(this);
                return false;
            }
            finally
            {

            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion





    }
}

