using System;
using Autofac;
using ShareMarket.BusinessLogic.Libs;
using ShareMarket.BusinessLogic.Tasks;
using ShareMarket.Core;
using ShareMarket.DataAccess.Repository;
using ShareMarket.Utility.Utilities;

namespace ShareMarket.BusinessLogic.Message
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
            using (
                IDataRepository<QueuedSms> queuedSmsContext =
                    GlobalUtil.Container.Resolve<IDataRepository<QueuedSms>>())
            {
                var queuedSmsList = GlobalLib.GetUnsentSms();
                foreach (var queuedSms in queuedSmsList)
                {
                    try
                    {
                        Utility.Message.SmsSender.SendSms(queuedSms.MobileNumber, queuedSms.Body);
                        queuedSms.SentOn = DateTime.UtcNow;
                    }
                    catch (Exception exc)
                    {
                        exc.LogError(this);
                        //LogManager.Instance.Error(string.Format("Error sending sms. {0}", exc.Message), exc);
                    }
                    finally
                    {
                        queuedSms.SentTries = queuedSms.SentTries + 1;
                        //save queued sms status
                        queuedSmsContext.Update(queuedSms);
                        queuedSmsContext.SaveChanges();
                    }
                }
            }
        }
    }
}
