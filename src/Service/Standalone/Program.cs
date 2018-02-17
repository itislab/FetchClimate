using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Standalone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the URI to use for the local host:
            string baseUri = "http://localhost:8080";

            Microsoft.Research.Science.FetchClimate2.AutoRegistratingTraceSource.RegisterTraceListener(new ConsoleTraceListener());            

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
            Console.ReadLine();
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var buf = File.ReadAllBytes(Path.Combine("dll", args.Name));
                Assembly ass = Assembly.Load(buf);
                Console.WriteLine("{0} loaded",args.Name);
                return ass;
            }
            catch (System.IO.FileNotFoundException) {
                Console.WriteLine("{0} NOT LOADED AS FILE NOT FOUND", args.Name);
                return null;
            }            
        }
    }
}
