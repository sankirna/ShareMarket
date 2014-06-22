using System;
using System.Collections.Generic;
using System.Text;

namespace ShareMarket.Utility.Message
{
    public class MessageTokenProvider
    {
        public static void AddCustomerTokens(IList<MessageToken> tokens, Customer customer, bool? IsCheckOutRegister = false)
        {
            //tokens for Customer
            tokens.Add(new MessageToken("Customer.FirstName", customer.FirstName));
            tokens.Add(new MessageToken("Customer.LastName", customer.LastName));
            tokens.Add(new MessageToken("Customer.Email", customer.Email));
            tokens.Add(new MessageToken("Customer.Password", customer.DecryptPassword));
            tokens.Add(new MessageToken("Customer.Verification", GetVerificationText(customer, IsCheckOutRegister)));
            tokens.Add(new MessageToken("Customer.VerificationCode", customer.VerificationCode));
            if (IsCheckOutRegister == false)
            {
                tokens.Add(new MessageToken("Customer.Text", "click on:"));
            }
            else
            {
                tokens.Add(new MessageToken("Customer.Text", "your"));
            }
            //Social Link
            tokens.Add(new MessageToken("SocialMedia.Facebook", SettingManager.Instance.Get("Site.SocialMedia.Facebook").Value));
            tokens.Add(new MessageToken("SocialMedia.Twitter", SettingManager.Instance.Get("Site.SocialMedia.Twitter").Value));
        }

        public static string GetVerificationText(Customer customer, bool? IsCheckOutRegister = false)
        {
            if (IsCheckOutRegister == false)
            {
                return GetVerificationMessage(string.Format("<a href='{0}'>{0}</a>", customer.VerificationLink));
            }
            else
            {
                return string.Format("{0} <br>{1}</b>", "Email Verification Code:", customer.VerificationCode);
            }
        }

        public static string GetVerificationMessage(string Message)
        {
            return string.Format("<tr><td style='font-family:Verdana, Arial, Helvetica, sans-serif; font-size:14px; color:#666666;'>{0}</td></tr>", Message);
        }

        public static void AddContactUsTokens(IList<MessageToken> tokens, ContactUs contactUs)
        {
            tokens.Add(new MessageToken("ContactUs.Name", contactUs.Name));
            tokens.Add(new MessageToken("ContactUs.OutletName", contactUs.OutletName));
            tokens.Add(new MessageToken("ContactUs.Phone", contactUs.Phone));
            tokens.Add(new MessageToken("ContactUs.Email", contactUs.Email));
            tokens.Add(new MessageToken("ContactUs.Comments", contactUs.Comments));
            tokens.Add(new MessageToken("ContactUs.ContactSubject", contactUs.ContactSubject.Name));
        }

        public static void AddOrderTokens(IList<MessageToken> tokens, Order order, bool displayCustomerAddress = true)
        {
            //tokens for outlet
            tokens.Add(new MessageToken("Outlet.Name", order.Outlet.Name));
            tokens.Add(new MessageToken("Customer.FirstName", order.Customer.FirstName));

            if (order.ShippingAddress != null)
            {
                tokens.Add(new MessageToken("Customer.Location", order.ShippingAddress.Location));
            }
            else
            {
                tokens.Add(new MessageToken("Customer.Location", order.Outlet.Location.Name));
            }

            tokens.Add(new MessageToken("Outlet.Location", order.Outlet.Location.Name));
            if (order.Outlet.OutletContacts != null)
            {
                var contact = order.Outlet.OutletContacts.FirstOrDefault();
                if (contact != null)
                {
                    tokens.Add(new MessageToken("Outlet.Contact", contact.ContactPhone));
                }
            }

            //Order delivery time
            var deliveryTime = "";
            if (order.OrderDeliveryTypeId == (int)DeliveryType.HomeDelivery)
            {
                deliveryTime = order.Outlet.HomeDeliveryTime;
            }
            else if (order.OrderDeliveryTypeId == (int)DeliveryType.TakeAway)
            {
                deliveryTime = order.Outlet.TakeAwayTime;
            }
            tokens.Add(new MessageToken("Outlet.DeliveryTime", deliveryTime));

            //tokens for order details
            tokens.Add(new MessageToken("Order.Id", order.Id.ToString()));
            tokens.Add(new MessageToken("Order.Detail", PrepareOrderDetail(order, displayCustomerAddress)));
            tokens.Add(new MessageToken("Order.Detail.Sms", PrepareOrderDetailSms(order)));

            //Order location
            var locationName = string.Empty;
            if (order.ShippingAddress != null)
            {
                locationName = order.ShippingAddress.Location;
            }
            else if (order.BillingAddress != null)
            {
                locationName = order.BillingAddress.Location;
            }
            tokens.Add(new MessageToken("Order.Location", locationName));

            tokens.Add(new MessageToken("SocialMedia.Facebook", SettingManager.Instance.Get("Site.SocialMedia.Facebook").Value));
            tokens.Add(new MessageToken("SocialMedia.Twitter", SettingManager.Instance.Get("Site.SocialMedia.Twitter").Value));

            tokens.Add(new MessageToken("Order.AcceptLink", OrderLinks(order.Id, 2, "OrderAcceptLink")));
            tokens.Add(new MessageToken("Order.DeclineLink", OrderLinks(order.Id, 3, "OrderDeclineLink")));
        }
        
        private static string OrderLinks(int OrderId, int StatusId, string LinkText)
        {
            return string.Format("<a href='{0}order/OrderStatusActivity/?OrderId={1}&StatusId={2}'>{3}</a>", SettingManager.Instance.Get("Site.Url").Value, OrderId, StatusId, LinkText);
        }

        private static string PrepareOrderDetailSms(Order order, bool displayCustomerAddress = true)
        {
            StringBuilder sbDetail = new StringBuilder();

            if (order.OrderDeliveryTypeId == (int)DeliveryType.HomeDelivery && displayCustomerAddress == true)
            {
                if (order.OrderDeliveryTypeId == (int)DeliveryType.HomeDelivery)
                {
                    sbDetail.AppendFormat("Delivery Address : {0} ", order.ShippingAddress.DisplayAddress());
                }
            }

            //Order items
            sbDetail.AppendFormat("Order : ");
            foreach (var item in order.OrderItems)
            {
                sbDetail.AppendFormat("{0} - Rs.{1} - {2} - Rs.{3} ",
                                       item.Product.GetProductName(),
                                       item.Product.Price.ToString("0.00"),
                                       item.Quantity,
                                       (item.Quantity * item.ItemPrice).ToString("0.00"));

                foreach (var Orderitems in OrderExtensions.GetCollectionAttributeItems(item.OrderItemAttributeItems, "Rs."))
                {
                    sbDetail.AppendFormat("{0} {1},", Orderitems.Key, Orderitems.Value);
                }
            }

            sbDetail.AppendFormat("Sub Total : Rs. {0} ", order.SubTotal.ToString("0.00"));

            if (!string.IsNullOrEmpty(order.CouponCode))
            {
                sbDetail.AppendFormat("Coupon Code : {0}", order.CouponCode);
            }

            if (!string.IsNullOrEmpty(order.Comment))
            {
                sbDetail.AppendFormat("Order Comments : {0} ", order.Comment);
            }

            if (order.DiscountTotal > 0)
            {
                sbDetail.AppendFormat("Discount Total : Rs. {0} ", order.DiscountTotal.ToString("0.00"));
            }

            if (order.Outlet.HomeDeliveryCharge != null)
            {
                sbDetail.AppendFormat("Home Delivery Charge : Rs. {0} ", Convert.ToDecimal(order.Outlet.HomeDeliveryCharge).ToString("0.00"));
            }

            foreach (var taxitem in order.OrderTaxes)
            {
                sbDetail.AppendFormat("{0} : Rs. {1} ", taxitem.TaxName, Convert.ToDecimal(taxitem.TaxAmount).ToString("0.00"));
            }

            sbDetail.AppendFormat("Order Total : Rs. {0}", order.OrderTotal.ToString("0.00"));

            return sbDetail.ToString();
        }

        private static string PrepareOrderDetail(Order order, bool displayCustomerAddress = true)
        {
            StringBuilder sbDetail = new StringBuilder();

            sbDetail.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
            sbDetail.Append("<tr>");
            sbDetail.Append("<td width='139' valign='top'>Delivery Type</td>");
            sbDetail.Append("<td width='13' valign='top'>:</td>");
            sbDetail.Append("<td width='448' valign='top'>" + order.OrderDeliveryType.Name + "</td>");
            sbDetail.Append("</tr>");

            sbDetail.Append("<tr>");
            sbDetail.Append("<td colspan='3'>&nbsp;</td>");
            sbDetail.Append("</tr>");


            if (order.OrderDeliveryTypeId == (int)DeliveryType.HomeDelivery && displayCustomerAddress == true)
            {
                sbDetail.Append("<tr>");
                sbDetail.Append("<td valign='top'>Delivery Address</td>");
                sbDetail.Append("<td valign='top'>:</td>");
                sbDetail.Append("<td valign='top'>");
                sbDetail.AppendFormat("{0} {1}", order.ShippingAddress.DisplayAddress(), "<br />");
                sbDetail.Append("</td>");
                sbDetail.Append("</tr>");
                sbDetail.Append("<tr>");
                sbDetail.Append("<td colspan='3'>&nbsp;</td>");
                sbDetail.Append("</tr>");
            }
            //else if (order.OrderDeliveryTypeId == (int)DeliveryType.TakeAway)
            //{
            //    sbDetail.AppendFormat("{0} {1}", order.BillingAddress.DisplayAddress(), "<br />");
            //}


            sbDetail.Append("<tr>");
            sbDetail.Append("<td valign='top'>Order</td>");
            sbDetail.Append("<td valign='top'>:</td>");
            sbDetail.Append("<td valign='top' style='font-size: 12px;'>");
            sbDetail.Append(GetItems(order));
            sbDetail.Append("</td>");
            sbDetail.Append("</tr>");

            sbDetail.Append("<tr>");
            sbDetail.Append("<td>&nbsp;</td>");
            sbDetail.Append("</tr>");
            sbDetail.Append("<tr>");
            sbDetail.Append("<td valign='top'>Sub Total</td>");
            sbDetail.Append("<td>:</td>");
            sbDetail.Append("<td valign='top' style='text-align:right;'>Rs. " + order.SubTotal.ToString("0.00") + "</td>");
            sbDetail.Append("</tr>");

            if (!string.IsNullOrEmpty(order.CouponCode))
            {
                sbDetail.Append("<tr>");
                sbDetail.Append("<td>&nbsp;</td>");
                sbDetail.Append("</tr>");
                sbDetail.Append("<tr>");
                sbDetail.Append("<td valign='top'>Coupon Code</td>");
                sbDetail.Append("<td>:</td>");
                sbDetail.Append("<td valign='top' style='text-align:right;''>" + order.CouponCode + "</td>");
                sbDetail.Append("</tr>");
            }

            if (Convert.ToDecimal(order.DiscountTotal) > 0)
            {
                sbDetail.Append("<tr>");
                sbDetail.Append("<td>&nbsp;</td>");
                sbDetail.Append("</tr>");
                sbDetail.Append("<tr>");
                sbDetail.Append("<td align='right' >Discount </td>");
                sbDetail.Append("<td>:</td>");
                sbDetail.Append("<td align='right' valign='top'>Rs. " + order.DiscountTotal.ToString("0.00") + "</td>");
                sbDetail.Append("</tr>");
            }

            foreach (var taxitem in order.OrderTaxes)
            {
                sbDetail.Append("<tr>");
                sbDetail.Append("<td>&nbsp;</td>");
                sbDetail.Append("</tr>");
                sbDetail.Append("<tr>");
                sbDetail.Append("<td  valign='top'>" + taxitem.TaxName + "</td>");
                sbDetail.Append("<td>:</td>");
                sbDetail.Append("<td valign='top' style='text-align:right;'>Rs. " + Convert.ToDecimal(taxitem.TaxAmount).ToString("0.00") + "</td>");
                sbDetail.Append("</tr>");
            }

            if (order.Outlet.HomeDeliveryCharge != null)
            {
                sbDetail.Append("<tr>");
                sbDetail.Append("<td>&nbsp;</td>");
                sbDetail.Append("</tr>");
                sbDetail.Append("<tr>");
                sbDetail.Append("<tdvalign='top'>Delievery charge :</td>");
                sbDetail.Append("<td>:</td>");
                sbDetail.Append("<td  valign='top' style='text-align:right;'>Rs. " + Convert.ToDecimal(order.Outlet.HomeDeliveryCharge).ToString("0.00") + "</td>");
                sbDetail.Append("</tr>");
            }

            sbDetail.Append("<tr>");
            sbDetail.Append("<td>&nbsp;</td>");
            sbDetail.Append("</tr>");
            sbDetail.Append("<tr>");
            sbDetail.Append("<td valign='top'>Total</td>");
            sbDetail.Append("<td>:</td>");
            sbDetail.Append("<td  align='right' valign='top'>Rs. " + order.OrderTotal.ToString("0.00") + "</td>");
            sbDetail.Append("</tr>");

            if (!string.IsNullOrEmpty(order.Comment))
            {
                sbDetail.Append("<tr>");
                sbDetail.Append("<td>&nbsp;</td>");
                sbDetail.Append("</tr>");
                sbDetail.Append("<tr>");
                sbDetail.Append("<td valign='top'>Order Comments</td>");
                sbDetail.Append("<td>:</td>");
                sbDetail.Append("<td valign='top'>" + order.Comment + "</td>");
                sbDetail.Append("</tr>");
            }

            sbDetail.Append("</table>");

            return sbDetail.ToString();
        }

        private static string GetItems(Order order)
        {
            StringBuilder retValue = new StringBuilder();
            retValue.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");

            retValue.Append("<tr>");
            retValue.Append("<td width='340'>Items</td>");
            retValue.Append("<td width='80' style='text-align:right;'>Qty</td>");
            retValue.Append("<td width='100' style='text-align:right;'>Price</td>");
            retValue.Append("<td width='100' style='text-align:right;'>Total</td>");
            retValue.Append("</tr>");


            foreach (var item in order.OrderItems)
            {
                var product = ProductManager.Instance.Get(item.ProductId);
                retValue.Append("<tr>");
                retValue.Append("<td colspan='4' height='20'>&nbsp;</td>");
                retValue.Append("</tr>");
                retValue.Append("<tr>");
                retValue.Append("<td>");
                retValue.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                retValue.Append("<tr>");
                retValue.Append("<td style='font-size:14px; color:#333333;'> " + product.GetProductName() + "</td>");
                retValue.Append("</tr>");
                retValue.Append("<tr>");
                retValue.Append("<td>&nbsp;</td>");
                retValue.Append("</tr>");
                retValue.Append("<tr>");
                retValue.Append("<td>");

                retValue.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                foreach (var Orderitems in OrderExtensions.GetCollectionAttributeItems(item.OrderItemAttributeItems))
                {
                    retValue.Append("<tr>");
                    retValue.Append("<td valign='top' style='font-weight:bold;'><b>" + Orderitems.Key + "</b> </td>");
                    retValue.Append("<td valign='top'>&nbsp;</td>");
                    retValue.Append("<td valign='top'> " + Orderitems.Value + " </td>");
                    retValue.Append("</tr>");
                }
                retValue.Append("</table>");

                retValue.Append("</td>");
                retValue.Append("</tr>");
                retValue.Append("</table>");

                retValue.Append("</td>");
                retValue.Append("<td valign='top' style='text-align:right;'>" + item.Quantity + "</td>");
                retValue.Append("<td valign='top' style='text-align:right;'>Rs. " + item.ItemPrice.ToString("0.00") + "</td>");
                retValue.Append("<td valign='top' style='text-align:right;'>Rs. " + (item.Quantity * item.ItemPrice).ToString("0.00") + "</td>");
                retValue.Append("</tr>");

            }

            retValue.Append(" </table>");
            return retValue.ToString();
        }

        public static void AddBanquetEnquiryTokens(IList<MessageToken> tokens, BanquetEnquiry banquetEnquiry)
        {
            tokens.Add(new MessageToken("BanquetEnquiry.Name", banquetEnquiry.Name));
            tokens.Add(new MessageToken("BanquetEnquiry.Email", banquetEnquiry.EmailId));
            tokens.Add(new MessageToken("BanquetEnquiry.Mobile", banquetEnquiry.MobileNumber));
            tokens.Add(new MessageToken("BanquetEnquiry.OccassionType", banquetEnquiry.OccasionTypes.Name));
            tokens.Add(new MessageToken("BanquetEnquiry.OccassionDate", banquetEnquiry.OccasionDate.ToString()));
            tokens.Add(new MessageToken("BanquetEnquiry.NoOfPerson", banquetEnquiry.NoOfPerson.ToString()));
            tokens.Add(new MessageToken("OutletBanquet.BanquetName", banquetEnquiry.OutletBanquets.Name));
            tokens.Add(new MessageToken("Outlet.Name", banquetEnquiry.OutletBanquets.Outlet.Name));
            
                
        }

        public static void AddBanquetEnquiryTokensForEndUser(IList<MessageToken> tokens, BanquetEnquiry banquetEnquiry)
        {
            tokens.Add(new MessageToken("BanquetEnquiry.Name", banquetEnquiry.Name));
          
        }

    }
}
