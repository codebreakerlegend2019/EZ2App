using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTwoJuetengApp.Models.AuthRelated
{
    public class LoginResult
    {
        public string Nickname { get; set; }
        public string Token { get; set; }
        public string LogginedUser { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
