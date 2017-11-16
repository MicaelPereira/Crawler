using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Core;

namespace Crawler.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Start Process");
            BusinesExportGust crawler = new BusinesExportGust();
            crawler.LoadStartups();
            System.Console.WriteLine("That's all");
            System.Console.ReadKey();
        }
    }
}
