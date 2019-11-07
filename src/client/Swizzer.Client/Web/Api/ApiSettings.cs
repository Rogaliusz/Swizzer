using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Web.Api
{
    public class ApiSettings
    {
        public string Token { get; set; }
        public string Address { get; set; } = "https://localhost:44357/";
    }
}
