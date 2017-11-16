using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler.Core
{
    public class Robo
    {
        public ClientWeb clientWeb { get; set; }
        public HtmlDocument HttpGet(string url)
        {
            lock (this)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(clientWeb.DownloadString(url));

                return htmlDocument;
            }
        }

        public HtmlDocument HttpPost(string url, NameValueCollection parametros)
        {

            var htmlDocument = new HtmlDocument();
            byte[] pagina = clientWeb.UploadValues(url, parametros);
            htmlDocument.LoadHtml(Encoding.Default.GetString(pagina, 0, pagina.Count()));

            return htmlDocument;
        }
    }
}
