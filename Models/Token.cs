using System;

namespace WebServer
{
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public Result result { get; set; }

    }

    public class Result
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}
