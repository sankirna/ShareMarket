using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareMarket.Core;
using ShareMarket.Utility.Message;

namespace ShareMarket.BusinessLogic.Message
{
    public static class CustomerMessageHelper
    {
        public static int SendOrderPlacedCustomerNotification(string messageTemplateName, UserProfile userProfile)
        {
            int returnValue = 0;

            if (userProfile == null)
                throw new ArgumentNullException("userProfile");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddCustomerTokens(tokens, userProfile);

            //Send email
            //var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("Customer.RegistrationNotification", MessageTemplateType.Email);
            //if (emailMessageTemplate != null)
            //{
                var toEmail = "";
                var toName = string.Format("{0} {1}", userProfile.UserName, userProfile.UserName);
                if (!string.IsNullOrEmpty(toEmail))
                {
                    returnValue = MessageHelper.SendNotification(new MessageTemplate(), tokens, toEmail, toName);
                }
           // }
            return 0;
        }

       

    }
}
