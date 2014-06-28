﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarket.Core
{
    [Table("QueuedSms")]
    public class QueuedSms : BaseEntityId
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }

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
