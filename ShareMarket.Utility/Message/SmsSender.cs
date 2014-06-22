using System;
using System.IO;
using System.Net;

namespace ShareMarket.Utility.Message
{
    public partial class SmsSender
    {
        public static void SendSms(string mobileNumber, string message)
        {
            string stringPost = "";//String.Format(SettingManager.Instance.Get("Sms.Post.Parameters").Value, mobileNumber, message);
            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;

            try
            {
                string stringResult = string.Empty;
                string url = "";//SettingManager.Instance.Get("Sms.Url").Value;
                objWebRequest = (HttpWebRequest)WebRequest.Create(url);
                objWebRequest.Method = "POST";

                objWebRequest.ContentType = "application/x-www-form-urlencoded";

                using (StreamWriter objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream()))
                {
                    objStreamWriter.Write(stringPost);
                    objStreamWriter.Flush();
                    objStreamWriter.Close();

                    objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();

                    using (StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream()))
                    {
                        stringResult = objStreamReader.ReadToEnd();
                        objStreamReader.Close();
                        if (!stringResult.Contains("OK"))
                            throw new Exception(stringResult);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                objWebRequest = null;
                objWebResponse = null;
            }
        }
    }
}
