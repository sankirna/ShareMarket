using System;
using System.Collections.Generic;
using ShareMarket.Core;

namespace ShareMarket.BusinessLogic.Message
{
    public class MessageTokenProvider
    {
        public static void AddCustomerTokens(IList<MessageToken> tokens, UserProfile userProfile)
        {
            //tokens for Customer
            tokens.Add(new MessageToken("Customer.UserName", userProfile.UserName));
        }

       
    }
}
