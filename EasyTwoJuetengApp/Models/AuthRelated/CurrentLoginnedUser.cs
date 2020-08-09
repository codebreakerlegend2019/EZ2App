using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTwoJuetengApp.Models.AuthRelated
{
    public class CurrentLoginnedUser
    {
        public string Token { get; set; }
        public string Nickname { get; set; }
        public string FullName { get; set; }

    }
}
