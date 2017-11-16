using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public class ClientWeb : WebClient
    {
        public CookieContainer _cookie = new CookieContainer();
        public bool _allowAutoRedirect;

        protected override WebRequest GetWebRequest(Uri address)
        {

            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).ServicePoint.Expect100Continue = false;
                (request as HttpWebRequest).CookieContainer = _cookie;
                (request as HttpWebRequest).KeepAlive = false;
                (request as HttpWebRequest).AllowAutoRedirect = _allowAutoRedirect;
            }

            return request;
        }
    }
}
