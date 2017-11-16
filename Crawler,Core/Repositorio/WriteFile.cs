using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Crawler.Core
{
    public static class WriteFile
    {
        public static void GenerateCsharpCodeForList(List<Startup> list, string project)
        {
            StreamWriter print;
            #region Verifica Caminho
            string pastaAplicacao = Environment.CurrentDirectory + @"\" + project + @"\CsharpList";
            if (!Directory.Exists(pastaAplicacao))
            {
                Directory.CreateDirectory(pastaAplicacao);
            }
            string nomeArquivo = pastaAplicacao + @"\list.cs";
            #endregion
            print = File.CreateText(nomeArquivo);

            print.WriteLine("List<Startup> startups = new List<Startup>();");
            int i = 0;
            foreach (var startup in list)
            {                
                string nameObj = string.Format("obj{0}", i);
                print.WriteLine(string.Format("Startup {0} = new Startup();", nameObj));
                print.WriteLine(string.Format("{0}.Id = {1};", nameObj, i + 1));
                print.WriteLine(string.Format("{0}.Title = \"{1}\";", nameObj, startup.Title));
                print.WriteLine(string.Format("{0}.Location = \"{1}\";", nameObj, startup.Location));
                print.WriteLine(string.Format("{0}.TypeOfStartup = \"{1}\";", nameObj, startup.TypeOfStartup));
                print.WriteLine(string.Format("{0}.Overview = \"{1}\";", nameObj, startup.Overview));
                print.WriteLine(string.Format("startups.Add({0});", nameObj));
                i++;
            }

            
            print.Close();

        }
    }
}
