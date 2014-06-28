using System.ComponentModel.DataAnnotations.Schema;
using ShareMarket.Core.Enums;

namespace ShareMarket.Core
{
    /// <summary>
    /// Represents the MessageTemplate
    /// </summary>
    public class MessageTemplate : BaseEntityId
    {
        #region Properties



        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the Message Template 
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets IsActive
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Custom properties

        [NotMapped]
        public MessageTemplateType MessageTemplateType
        {
            get
            {
                return (MessageTemplateType)this.Type;
            }
            set
            {
                this.Type = (int)value;
            }
        }

        #endregion


    }
}
