using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _4Mat.Logging
{
    public class Logger
    {
        public Logger()
        {
        }
        public static void Log(Exception ex)
        {
            Log(HttpCommand.None, ex.ToString());
        }
        public static void Log(HttpCommand command, HttpRequest request)
        {
            Log(command, GetHttpRequestData(request));
        }
        public static void Log(string message)
        {
            Log(HttpCommand.None, message);
        }
        public static void Log(HttpCommand command, string message)
        {
            //TODO: Logging to storage
        }

        public static string GetHttpRequestData(HttpRequest request)
        {
            //TODO: Log further information about the request and context it is running in
            return request.Path.HasValue ? request.Path.Value : "No HTTP Path";
        }
    }
}
