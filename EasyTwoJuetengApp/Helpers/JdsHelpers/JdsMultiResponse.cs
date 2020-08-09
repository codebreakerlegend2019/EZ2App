using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EasyTwoJuetengApp.Helpers.JdsHelpers
{
    public class JdsMultiReponse<T>
    {
        public string RequestContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<T> Data { get; set; }
    }
}
