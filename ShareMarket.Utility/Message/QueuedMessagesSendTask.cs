using System;

namespace ShareMarket.Utility.Message
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
            var queuedEmails = QueuedEmailManager.Instance.GetUnsentEmails();
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
                    EmailSender.SendEmail(queuedEmail.Subject, queuedEmail.Body,
                       queuedEmail.From, queuedEmail.FromName, queuedEmail.To, queuedEmail.ToName, bcc, cc);

                    queuedEmail.SentOn = DateTime.UtcNow;
                }
                catch (Exception exc)
                {
                    LogManager.Instance.Error(string.Format("Error sending e-mail. {0}", exc.Message), exc);
                }
                finally
                {
                    queuedEmail.SentTries = queuedEmail.SentTries + 1;
                    //save queued email status
                    QueuedEmailManager.Instance.Save(queuedEmail);
                }
            }
        }
    }
}
