using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarket.Core
{
    [Table("QueuedEmail")]
    public class QueuedEmail : BaseEntityId
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the From
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the FromName
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the To
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the ToName
        /// </summary>
        public string ToName { get; set; }

        /// <summary>
        /// Gets or sets the CC
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// Gets or sets the Bcc
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// Gets or sets the Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn datetime
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the SentTries
        /// </summary>
        public int SentTries { get; set; }

        /// <summary>
        /// Gets or sets the SentOn datetime
        /// </summary>
        public DateTime? SentOn { get; set; }
        #endregion
    }
}
