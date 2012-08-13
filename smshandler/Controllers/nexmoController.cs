using System.Web.Mvc;
using System.Net;
using System.IO;
using System;
using log4net;
using log4net.Config;
namespace smshandler.Controllers
{
    /// <summary>
    /// Called via the route api/nexmo
    /// 
    /// You can find the nexmo documentation here: http://nexmo.com/documentation/index.html
    /// Handling the inbound message: http://nexmo.com/documentation/index.html#mo
    /// Creating the outgoing text message: http://nexmo.com/documentation/index.html#txt
    /// </summary>
    public class NexmoController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string commandFile = "nexmoCommandFile.txt";
        HttpWebRequest objWebRequest = null;
        //Set uesrname
        String username = "";
        //Set password
        String password = "";
        //Set from phone number
        String fromPhone = "MyCompany20";
        //Set to phone number
        String ToPhone = "";
        // GET api/values 
        public string ProcessText()
        {
            // Log Text Message
            log.Debug("Application started");
            // Read the command file and Create Outgoing Text Message 
            string fileName = Server.MapPath("~") + @"\" + commandFile;
            String message = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileName))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                 message = message + line;
                }
            }
            log.Info("to number:" + ToPhone + ", from number:" + fromPhone + ", messageId:, message timestamp:, text:"+message+"");
            SendOutgoingMessage(message);
            return "Success";
        }
        private void SendOutgoingMessage(string message)
        {
        
            objWebRequest = (HttpWebRequest)WebRequest.Create("http://rest.nexmo.com/sms/json?username=" + username + "&password=" + password + "&from=" + fromPhone + "&to=" + ToPhone + "&text=" + message );
            objWebRequest.Method = "POST";
            var json = GenerateMessageUrl(message);
           // make an http request to nexmo here. 
        }
        private  string GenerateMessageUrl(string message)
        {
            HttpWebResponse response = (HttpWebResponse)objWebRequest.GetResponse();
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            String responsestring = streamRead.ReadToEnd();
            return responsestring; //should return the json string as specified in the nexmo documentation
        }

        /// <summary>
        /// Use this method as an easy way to test the outgoing sms message 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetMessageUrl(string message)
        {
           return GenerateMessageUrl(message);
        }
    }
}