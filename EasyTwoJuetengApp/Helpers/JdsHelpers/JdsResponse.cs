﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EasyTwoJuetengApp.Helpers.JdsHelpers
{
    public class JdsResponse
    {
        public string RequestContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}