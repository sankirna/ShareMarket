using System;
using log4net;
using Microsoft.Build.Framework;

namespace ShareMarket.Utility.Message
{
    /// <summary>
    /// Represents a task for sending queued sms 
    /// </summary>
    public partial class QueuedSmsSendTask : ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            var queuedSmsList = QueuedSmsManager.Instance.GetUnsentSms();
            foreach (var queuedSms in queuedSmsList)
            {
                try
                {
                    SmsSender.SendSms(queuedSms.MobileNumber, queuedSms.Body);
                    queuedSms.SentOn = DateTime.UtcNow;
                }
                catch (Exception exc)
                {
                    LogManager.Instance.Error(string.Format("Error sending sms. {0}", exc.Message), exc);
                }
                finally
                {
                    queuedSms.SentTries = queuedSms.SentTries + 1;
                    //save queued sms status
                    QueuedSmsManager.Instance.Save(queuedSms);
                }
            }
        }
    }
}
