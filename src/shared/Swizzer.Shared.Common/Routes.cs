using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common
{
    public static class Routes
    {
        public const string Api = "api/";
        public static class Users
        {
            public const string Main = Api + "users/";
            public const string Login = Main + "login/";
        }
    }
}
