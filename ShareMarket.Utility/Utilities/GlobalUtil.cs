using System.Collections.Generic;
using System.Text;
using Autofac;
using log4net;
using System;
using System.Configuration;
using System.Reflection;
using MobileSurvey.Utils.Utilities;


namespace ShareMarket.Utility.Utilities
{
    public static class GlobalUtil
    {
        #region "Public Properties"

        public static IContainer Container { get; set; }

        /// <summary>
        /// Return the connection string
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            }
        }

        public static List<string> Operator = new List<string>()
        {
            ">","<","="
        };
        #endregion

        #region "Public Method(s)"

        /// <summary>
        /// Method log info into DB using Log4net
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callingType"></param>
        public static void LogMessage(string message, object callingType)
        {
            var log = LogManager.GetLogger(callingType.GetType());
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        /// <summary>
        /// Method log info into DB using Log4net
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public static void LogMessage(string message, Type type)
        {
            var log = LogManager.GetLogger(type);
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }
        
        /// <summary>
        /// Set App Setting Value in Class static members
        /// </summary>
        public static void SetAppSettingValue()
        {
            try
            {
                var appSettingMembers = typeof(AppSettingUtil).GetMembers(BindingFlags.Static | BindingFlags.Public);
                foreach (var member in appSettingMembers)
                {
                    Type type = member.GetType();
                    FieldInfo field = typeof(AppSettingUtil).GetField(member.Name, BindingFlags.Public | BindingFlags.Static);
                    field.SetValue(typeof(AppSettingUtil), ConfigurationManager.AppSettings[member.Name]);
                }

            }
            catch (Exception ex)
            {
                ex.LogError(typeof(GlobalUtil));
            }
        }

        /// <summary>
        /// Get unique code based on length mentioned
        /// </summary>
        /// <param name="length">int</param>
        /// <returns>string</returns>
        public static string GetUniqueCode(int length)
        {
            try
            {
                string guidResult = string.Empty;

                while (guidResult.Length < length)
                {
                    // Get the GUID.
                    guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
                }

                // Make sure length is valid.
                if (length <= 0 || length > guidResult.Length)
                    throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

                // Return the first length bytes.
                return guidResult.Substring(0, length);
            }
            catch (Exception ex)
            {
                ex.LogError(typeof(GlobalUtil));
            }
            return string.Empty;
        }

        /// <summary>
        /// set Log for web api method request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callingType"></param>
        public static void LogRequest(string message, object callingType)
        {
            try
            {
                var log = LogManager.GetLogger(callingType.GetType());
                log.Debug(message);
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message, callingType);
            }
        }

        /// <summary>
        /// Get Log Message from List with Formate tab
        /// </summary>
        /// <param name="actionName">Action Name</param>
        /// <param name="controllerName">Controller Name</param>
        /// <param name="logString">Key Value pair of model</param>
        /// <returns>string</returns>
        public static string GetLogMessageString(string actionName, string controllerName, params KeyValuePair<string, string>[] logString)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Action:" + actionName + "\t");
            sb.Append("Controller:" + controllerName + "\t");
            foreach (KeyValuePair<string, string> logStringPair in logString)
            {
                sb.Append(Convert.ToString(logStringPair.Key) + ":" + Convert.ToString(logStringPair.Value) + "\t");
            }
            return sb.ToString();
        }

        #endregion
    }
}
