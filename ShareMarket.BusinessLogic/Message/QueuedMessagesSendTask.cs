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
    /// Represents a task for sending queued message 
    /// </summary>
    public partial class QueuedMessagesSendTask : ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            using (
                IDataRepository<QueuedEmail> queuedEmailContext =
                    GlobalUtil.Container.Resolve<IDataRepository<QueuedEmail>>())
            {
                var queuedEmails = GlobalLib.GetUnsentEmails();
                foreach (var queuedEmail in queuedEmails)
                {
                    var bcc = String.IsNullOrWhiteSpace(queuedEmail.Bcc)
                        ? null
                        : queuedEmail.Bcc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var cc = String.IsNullOrWhiteSpace(queuedEmail.CC)
                        ? null
                        : queuedEmail.CC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    try
                    {
                        Utility.Message.EmailSender.SendEmail(queuedEmail.Subject, queuedEmail.Body,
                            queuedEmail.From, queuedEmail.FromName, queuedEmail.To, queuedEmail.ToName, bcc, cc);

                        queuedEmail.SentOn = DateTime.UtcNow;
                    }
                    catch (Exception exc)
                    {
                        exc.LogError(this);
                        // LogManager.Instance.Error(string.Format("Error sending e-mail. {0}", exc.Message), exc);
                    }
                    finally
                    {
                        queuedEmail.SentTries = queuedEmail.SentTries + 1;
                        //save queued email status
                        queuedEmailContext.Update(queuedEmail);
                        queuedEmailContext.SaveChanges();
                    }
                }
            }
        }
    }
}
