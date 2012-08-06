using System.Collections.Generic;
using System.Web.Http;

namespace smshandler.Controllers
{
    /// <summary>
    /// Called via the route api/nexmo
    /// </summary>
    public class NexmoController : ApiController
    {
        public static string commandFile = "nexmoCommandFile.txt";
        // GET api/values
        public string Get()
        {
            // Log Text Message
            // Read the command file and Create Outgoing Text Message
            CreateOutgoingMessage("message");
            return "Success";
        }

        private void CreateOutgoingMessage(string message)
        {
            
        }
    }
}