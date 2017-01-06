using System;
using System.Net;
using System.Net.Sockets;

namespace Networking
{
    public static class Helpers
    {
        //Took from stack overflow
        public static string GetLocalIpAddress()
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
    }
}

namespace Networking
{
    public static class User
    {
        public static string Username = "";
        public static string LocalIp = "";
        public static int PlayerId;
        public static string Character = "";
    }
}