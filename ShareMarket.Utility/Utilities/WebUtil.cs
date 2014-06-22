using System;
using System.Web;

namespace ShareMarket.Utility.Utilities
{
    public class WebUtil
    {
        public static string CurrentExecutingUrl
        {
            get { return string.Format("{0}://{1}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority); }
        }

        #region "Application Method(S)"

        /// <summary>
        /// Set Application Value. value With generic type
        /// </summary>
        /// <typeparam name="T">Generic Type of Value</typeparam>
        /// <param name="key">Application Enum</param>
        /// <param name="value">Value of the application Key</param>
        public static void SetApplicationValue<T>(string key, T value)
        {
            HttpContext.Current.Application[key] = value;
        }

        /// <summary>
        /// Get Application Value by Enum. Default Value with Generic type
        /// </summary>
        /// <typeparam name="T">Generic Type of Value</typeparam>
        /// <param name="key">Application Enum</param>
        /// <param name="defaultValue">if Application enum not found then default value return. Default Value With Generic type</param>
        /// <returns></returns>
        public static T GetApplicationValue<T>(string key, T defaultValue)
        {
            if (HttpContext.Current.Application[key] != null)
            {
                return (T)HttpContext.Current.Application[key];
            }
            return defaultValue;
        }

        #endregion

        #region "Session Method(S)"

        /// <summary>
        /// Set Session Value. value is generic type
        /// </summary>
        /// <typeparam name="T">Generic Type of Value</typeparam>
        /// <param name="key">String key</param>
        /// <param name="value">Get Session Value Get With session key. Value is Generic Type.</param>
        public static void SetSessionValue<T>(string key, T value)
        {
            HttpContext.Current.Session[key] = value;
        }

        /// <summary>
        /// get Session Value. value is generic type
        /// </summary>
        /// <typeparam name="T">Generic Type of Value</typeparam>
        /// <param name="key">string key</param>
        public static T GetSessionValue<T>(string key)
        {
            T sessionValue = default(T);
            if (IsSessionExists(key))
            {
                sessionValue = (T)HttpContext.Current.Session[key];
            }
            return sessionValue;
        }

        /// <summary>
        /// Remove Session by Session Key Enum
        /// </summary>
        /// <param name="key">Session Enum</param>
        public static void RemoveSessionValue(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        /// <summary>
        /// Removes all key-value from session
        /// </summary>
        public static void RemoveAll()
        {
            try
            {
                HttpContext.Current.Session.RemoveAll();
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
        }

        /// <summary>
        /// remove session value with a particular key
        /// </summary>
        /// <param name="key">String key</param>
        public static void Remove(string key)
        {
            try
            {
                if (IsSessionExists(key))
                    HttpContext.Current.Session.Remove(key);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
        }

        /// <summary>
        ///  Cancels the current session.
        /// </summary>
        public static void AbandonSession()
        {
            try
            {
                HttpContext.Current.Session.Abandon();
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
        }

        /// <summary>
        ///   Removes all keys and values from the session-state collection.
        /// </summary>
        public static void ClearSession()
        {
            try
            {
                HttpContext.Current.Session.Clear();
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
        }

        /// <summary>
        /// Returns weather Session is Expired or Not.
        /// </summary>
        /// <param name="key">String key</param>
        /// <returns>true : if session expired or else false.</returns>
        public static bool IsSessionExists(string key)
        {
            try
            {
                if (!IsSessionExpired())
                {
                    var httpSessionStateBase = HttpContext.Current;
                    return httpSessionStateBase.Session[key] != null;
                }

            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
            return false;
        }

        /// <summary>
        /// Returns weather Session is Expired or Not.
        /// </summary>
        /// <returns>true : if session expired or else false.</returns>
        public static bool IsSessionExpired()
        {
            try
            {
                var httpSessionStateBase = HttpContext.Current;
                return httpSessionStateBase.Session == null || httpSessionStateBase.Session.Keys.Count <= 0;
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
            return true;
        }

        #endregion

        #region "Cookies Method(S)"

        /// <summary>
        /// Get Cookies Value
        /// </summary>
        /// <typeparam name="T">Generic Type of Value</typeparam>
        /// <param name="key">Cookie Enum</param>
        /// <param name="defaultValue">Get Cookie Value Get With Cookie key. Value is Generic Type.</param>
        /// <returns></returns>
        public static string GetCookieValue(string key, string defaultValue)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[key] != null)
                {
                    return HttpContext.Current.Request.Cookies[key].Value;
                }
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
            return defaultValue;
        }

        /// <summary>
        /// Get Cookies Value
        /// </summary>
        /// <typeparam name="T">Generic Type of Value</typeparam>
        /// <param name="key">Cookie Enum</param>
        /// <param name="defaultValue">Get Cookie Value Get With Cookie key. Value is Generic Type.</param>
        /// <returns></returns>
        public static int GetCookieValue(string key, int defaultValue)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[key] != null)
                {
                    return HttpContext.Current.Request.Cookies[key].Value.ToIntFromString();
                }
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
            return defaultValue;
        }

        /// <summary>
        /// Set Cookies Value
        /// </summary>
        /// <typeparam name="T">Generic Type of Value</typeparam>
        /// <param name="key">Cookie Enum</param>
        /// <param name="defaultValue">Get Cookie Value Get With Cookie key. Value is Generic Type.</param>
        public static void SetCookieValue(string key, string defaultValue)
        {
            try
            {
                var cookie = new HttpCookie(key) { Value = defaultValue };
                if (string.IsNullOrEmpty(defaultValue))
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 24 * 365; //TODO make configurable
                    cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
        }

        /// <summary>
        /// Remove Cookies  with Key
        /// </summary>
        /// <param name="key">Cookie Enum</param>
        public static void RemoveCookies(string key)
        {
            try
            {
                var cookie = new HttpCookie(key) { Expires = DateTime.Now.AddMonths(-1) };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
        }

        /// <summary>
        /// Check For Existence of Cookies
        /// </summary>
        /// <param name="key">Cookie Enum</param>
        /// <returns></returns>
        public static bool IsCookieValueExist(string key)
        {
            try
            {
                return HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[key] != null;
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(WebUtil));
            }
            return false;

        }

        #endregion



    }
}
