using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace ShareMarket.Utility.Message
{
    public class MessageHelper
    {
        private static int SendNotification(MessageTemplate messageTemplate, IEnumerable<MessageToken> tokens, string toEmailAddress, string toName)
        {
            //retrieve localized message template data
            var subject = messageTemplate.Subject;
            var body = messageTemplate.Body;

            //Replace subject and body tokens 
            MessageTokenizer tokenizer = new MessageTokenizer();
            var subjectReplaced = tokenizer.Replace(subject, tokens, false);
            var bodyReplaced = tokenizer.Replace(body, tokens, false);

            var from = SettingManager.Instance.Get("Email.From").Value;
            var fromName = SettingManager.Instance.Get("Email.From.DisplayName").Value;

            var email = new QueuedEmail()
            {
                Priority = 5,
                From = from,
                FromName = fromName,
                To = toEmailAddress,
                ToName = toName,
                CC = string.Empty,
                Bcc = string.Empty,
                Subject = subjectReplaced,
                Body = bodyReplaced,
                CreatedOn = DateTime.UtcNow
            };

            email = QueuedEmailManager.Instance.Save(email);
            return email.Id;
        }

        private static int SendNotification(MessageTemplate messageTemplate, IEnumerable<MessageToken> tokens, string toMobileNumber)
        {
            //retrieve localized message template data
            var body = messageTemplate.Body;

            //Replace subject and body tokens 
            MessageTokenizer tokenizer = new MessageTokenizer();
            string bodyReplaced = tokenizer.Replace(body, tokens, false);
            bodyReplaced = GbmManager.Helper.HtmlRemovalHelper.StripTagsRegex(bodyReplaced);
            bodyReplaced = GetValidTokenString(bodyReplaced);

            var sms = new QueuedSms()
            {
                Priority = 5,
                MobileNumber = toMobileNumber,
                Body = bodyReplaced,
                CreatedOn = DateTime.UtcNow
            };

            sms = QueuedSmsManager.Instance.Save(sms);
            return sms.Id;
        }


        private static string GetValidTokenString(string tokenString)
        {
            Regex re = new Regex("[%?]"); //Regular expression includes character which breaks the sms content must be avoided by null value
            tokenString = re.Replace(tokenString, "");
            tokenString = tokenString.Replace("&", "and");
            return tokenString;
        }

        #region Customer notifications

        public static int SendOrderPlacedCustomerNotification(Order order)
        {
            int returnValue = 0;

            if (order == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddOrderTokens(tokens, order);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderPlaced.CustomerNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = order.Customer.Email;
                var toName = string.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName);
                if (!string.IsNullOrEmpty(toEmail))
                {
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
                }
            }

            //Send Sms
            var smsMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderPlaced.CustomerNotification", MessageTemplateType.Sms);
            if (smsMessageTemplate != null)
            {
                string mobileNumber = order.Customer.MobileNumber;
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    returnValue = SendNotification(smsMessageTemplate, tokens, mobileNumber);
                }
            }

            return returnValue;
        }

        public static int SendOrderAcceptedCustomerNotification(Order order)
        {
            int returnValue = 0;

            if (order == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddOrderTokens(tokens, order);

            //send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderAccepted.CustomerNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = order.Customer.Email;
                var toName = string.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName);
                if (!string.IsNullOrEmpty(toEmail))
                {
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
                }
            }

            //send sms
            var smsMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderAccepted.CustomerNotification", MessageTemplateType.Sms);
            if (smsMessageTemplate != null)
            {

                string mobileNumber = order.Customer.MobileNumber;
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    returnValue = SendNotification(smsMessageTemplate, tokens, mobileNumber);
                }
            }

            return returnValue;
        }

        public static int SendOrderCancelledCustomerNotification(Order order)
        {
            int returnValue = 0;

            if (order == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddOrderTokens(tokens, order);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderCancelled.CustomerNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = order.Customer.Email;
                var toName = string.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName);
                if (!string.IsNullOrEmpty(toEmail))
                {
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
                }
            }

            //Send sms
            var smsMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderCancelled.CustomerNotification", MessageTemplateType.Sms);
            if (smsMessageTemplate != null)
            {
                string mobileNumber = order.Customer.MobileNumber;
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    returnValue = SendNotification(smsMessageTemplate, tokens, mobileNumber);
                }
            }

            return returnValue;
        }

        public static int SendOrderDeclinedCustomerNotification(Order order)
        {
            int returnValue = 0;

            if (order == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddOrderTokens(tokens, order);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderDeclined.CustomerNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = order.Customer.Email;
                var toName = string.Format("{0} {1}", order.Customer.FirstName, order.Customer.LastName);
                if (!string.IsNullOrEmpty(toEmail))
                {
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
                }
            }

            //Send sms
            var smsMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderDeclined.CustomerNotification", MessageTemplateType.Sms);
            if (smsMessageTemplate != null)
            {
                string mobileNumber = order.Customer.MobileNumber;
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    returnValue = SendNotification(smsMessageTemplate, tokens, mobileNumber);
                }
            }

            return returnValue;
        }

        public static int SendCustomerRegisteredNotification(Customer customer, bool? IscheckOutRegister = false)
        {
            int returnValue = 0;

            if (customer == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddCustomerTokens(tokens, customer, IscheckOutRegister);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("Customer.RegistrationNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = customer.Email;
                var toName = string.Format("{0} {1}", customer.FirstName, customer.LastName);
                if (!string.IsNullOrEmpty(toEmail))
                {
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
                }
            }

            return returnValue;
        }

        public static int SendCustomerPasswordRecoveryNotification(Customer customer)
        {
            int returnValue = 0;

            if (customer == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddCustomerTokens(tokens, customer);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("Customer.ForgotPasswordNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = customer.Email;
                var toName = string.Format("{0} {1}", customer.FirstName, customer.LastName);
                if (!string.IsNullOrEmpty(toEmail))
                {
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
                }
            }
            return returnValue;
        }

        public static int SendCustomerEmailVerificationNotification(Customer customer)
        {
            int returnValue = 0;

            if (customer == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddCustomerTokens(tokens, customer);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("Customer.EmailVerificationNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = customer.Email;
                var toName = string.Format("{0} {1}", customer.FirstName, customer.LastName);
                if (!string.IsNullOrEmpty(toEmail))
                {
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
                }
            }

            return returnValue;
        }

        public static int SendCustomerMobileVerificationNotification(Customer customer)
        {
            int returnValue = 0;

            if (customer == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddCustomerTokens(tokens, customer);

            //Send sms
            var smsMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("Customer.MobileVerificationNotification", MessageTemplateType.Sms);
            if (smsMessageTemplate != null)
            {
                string mobileNumber = customer.MobileNumber;
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    returnValue = SendNotification(smsMessageTemplate, tokens, mobileNumber);
                }
            }
            return returnValue;
        }

        #endregion

        #region Outlet notifications

        public static int SendOrderPlacedOutletNotification(Order order)
        {
            int returnValue = 0;

            if (order == null)
                throw new ArgumentNullException("order");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddOrderTokens(tokens, order, false);

            var contact = order.Outlet.OutletContacts.FirstOrDefault();

            //send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderPlaced.OutletNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                if (contact != null)
                {
                    var toEmail = contact.EmailReceiver;
                    var toName = string.Format("{0}", contact.ContactName);
                    if (!string.IsNullOrEmpty(toEmail))
                        returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
                }
            }

            //send sms
            var smsMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("OrderPlaced.OutletNotification", MessageTemplateType.Sms);
            if (smsMessageTemplate != null)
            {
                if (contact != null)
                {
                    var toMobileNumber = contact.SmsReceiver;
                    if (!string.IsNullOrEmpty(toMobileNumber))
                        returnValue = SendNotification(smsMessageTemplate, tokens, toMobileNumber);
                }
            }

            return returnValue;
        }
        #endregion

        #region Common notifications

        public static int SendContactUsNotificationMessage(ContactUs contactUs)
        {
            int returnValue = 0;

            if (contactUs == null)
                throw new ArgumentNullException("ContactUs");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddContactUsTokens(tokens, contactUs);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("ContactUs", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = contactUs.Email;
                var toName = string.Format("{0}", contactUs.Name);
                if (!string.IsNullOrEmpty(toEmail))
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
            }

            return returnValue;
        }

        public static int SendFeedbackNotificationMessage(ContactUs contactUs)
        {
            int returnValue = 0;

            if (contactUs == null)
                throw new ArgumentNullException("Feedback");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddContactUsTokens(tokens, contactUs);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("Feedback", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = contactUs.Email;
                var toName = string.Format("{0}", contactUs.Name);
                if (!string.IsNullOrEmpty(toEmail))
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
            }

            return returnValue;
        }

        public static int SendAddRestaurantNotificationMessage(ContactUs contactUs)
        {
            int returnValue = 0;

            if (contactUs == null)
                throw new ArgumentNullException("ContactUs");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddContactUsTokens(tokens, contactUs);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("AddRestaurant", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = contactUs.Email;
                var toName = string.Format("{0}", contactUs.Name);
                if (!string.IsNullOrEmpty(toEmail))
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
            }

            return returnValue;
        }

        public static int SendAdevertiseWithUsNotificationMessage(ContactUs contactUs)
        {
            int returnValue = 0;

            if (contactUs == null)
                throw new ArgumentNullException("AdvertiseWithUs");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddContactUsTokens(tokens, contactUs);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("AdvertiseWithUs", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = contactUs.Email;
                var toName = string.Format("{0}", contactUs.Name);
                if (!string.IsNullOrEmpty(toEmail))
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
            }

            return returnValue;
        }

        #endregion

        #region Banquet Enquiry Notifications

        public static int SendBanquetEnqiryNotificationMessageToSalesAdmin(BanquetEnquiry banquetEnquiry)
        {
            int returnValue = 0;

            if (banquetEnquiry == null)
                throw new ArgumentNullException("BanquetEnquiry");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddBanquetEnquiryTokens(tokens, banquetEnquiry);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("BanquetEnquiry.EnquiryNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = ConfigurationManager.AppSettings["SalesEmail"];
                var toName = ConfigurationManager.AppSettings["SalesAdmin"];
                if (!string.IsNullOrEmpty(toEmail))
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
            }

            return returnValue;
        }


        public static int SendBanquetEnqiryNotificationMessageToEndUser(BanquetEnquiry banquetEnquiry)
        {
            int returnValue = 0;

            if (banquetEnquiry == null)
                throw new ArgumentNullException("BanquetEnquiry");

            //tokens
            var tokens = new List<MessageToken>();
            MessageTokenProvider.AddBanquetEnquiryTokensForEndUser(tokens, banquetEnquiry);

            //Send email
            var emailMessageTemplate = MessageTemplateManager.Instance.GetMessageTemplateByName("BanquetEnquiry.CustomerNotification", MessageTemplateType.Email);
            if (emailMessageTemplate != null)
            {
                var toEmail = banquetEnquiry.EmailId;
                var toName = banquetEnquiry.Name;
                if (!string.IsNullOrEmpty(toEmail))
                    returnValue = SendNotification(emailMessageTemplate, tokens, toEmail, toName);
            }

            return returnValue;
        }

        #endregion


    }
}
