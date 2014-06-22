using System;
using log4net;

namespace ShareMarket.Utility.Utilities
{
    public static class ExtensionUtil
    {

        /// <summary>
        /// Method Log error using Log4net
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="callingType"></param>
        public static void LogError(this Exception ex, object callingType)
        {
            var log = LogManager.GetLogger(callingType.GetType());
            if (log.IsErrorEnabled)
            {
                log.Error(ex.Message, ex);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Method Log error using Log4net
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="callingType"></param>
        public static void LogError(this Exception ex, Type callingType)
        {
            var log = LogManager.GetLogger(callingType);
            if (log.IsErrorEnabled)
            {
                log.Error(ex.Message, ex);
                throw new Exception(ex.Message, ex);
            }
        }

    
    }
}
