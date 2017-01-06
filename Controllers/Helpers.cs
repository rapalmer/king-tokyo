using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Collections.Specialized;

namespace Controllers.Helpers
{
    public static class Helpers
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        //Took from stack overflow
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public static string WebMessage(NameValueCollection data)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = wc.UploadValues("http://proj-309-yt-01.cs.iastate.edu/login.php", "POST", data);
                return Encoding.ASCII.GetString(result);
                //Console.WriteLine("\nResponse received was :\n{0}", encresult);
            }
        }
    }
}

namespace Controllers.User
{
    public static class User
    {
        public static string username = "";
        public static string localIp = "";
        public static string id = "";
        public static string character = "";
    }
}