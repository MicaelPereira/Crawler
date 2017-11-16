using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public class BusinesExportGust : Robo
    {
        public BusinesExportGust()
        {
            clientWeb = new ClientWeb();
        }

        public List<Startup> LoadStartups()
        {
            List<Startup> startups = new List<Startup>();
            Task[] taskArray = new Task[99];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew(() => {
                    var html = this.HttpGet(string.Format("https://gust.com/search/new?category=startups&page={0}&partial=results&utf8=%E2%9C%93", i + 1));
                    clientWeb = new ClientWeb();
                    startups.AddRange(this.ExtractHTMLToObject(html));
                });
            }
            Task.WaitAll(taskArray);

            WriteFile.GenerateCsharpCodeForList(startups, "Forum");

            return startups;
            
        }

        private List<Startup> ExtractHTMLToObject(HtmlAgilityPack.HtmlDocument html)
        {
            List<Startup> startups = new List<Startup>();
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();

            var tags = html.DocumentNode.Descendants().Where(n => n.Name == "ul").OrderBy(d => d.Id).ToList();
            
            foreach (var item in tags)
            {
                var divCorreta = item.InnerHtml.Contains("card-title");
                if (divCorreta)
                {
                    htmlDocument.LoadHtml(item.InnerHtml);

                    var rigthLi = htmlDocument.DocumentNode.DescendantsAndSelf().Where(x => x.Name == "li").ToList();

                    foreach (var li in rigthLi)
                    {
                        Startup art = new Startup();
                        art.Title = HtmlAgilityPack.HtmlEntity.DeEntitize(ConvertUTF(htmlDocument.DocumentNode.DescendantsAndSelf().FirstOrDefault(d => d.Attributes["class"] != null && d.Attributes["class"].Value == "card-title").InnerText.Replace("\n", "")));
                        var locationAndType = HtmlAgilityPack.HtmlEntity.DeEntitize(ConvertUTF(htmlDocument.DocumentNode.DescendantsAndSelf().FirstOrDefault(d => d.Attributes["class"] != null && d.Attributes["class"].Value == "card-secondary-subtitle").InnerText.Replace("\n", ""))).Split('·');
                        art.Location = locationAndType[0];
                        art.TypeOfStartup = locationAndType[1];
                        art.Overview = HtmlAgilityPack.HtmlEntity.DeEntitize(ConvertUTF(htmlDocument.DocumentNode.DescendantsAndSelf().FirstOrDefault(d => d.Attributes["class"] != null && d.Attributes["class"].Value == "card-content").InnerText.Replace("\n", "")));
                        startups.Add(art);
                    }
                }
            }

            return startups.ToList();
        }

        private string ConvertUTF(string text)
        {
            byte[] data = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(data);
        }

    }
    
   
}
