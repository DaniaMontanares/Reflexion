using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Pro.Interface;

namespace ClientProject
{
    class Program
    {
        static void Main(string[] args)
        {
            const string extensionsPath = @"C:\Users\Dania\source\repos\ConsoleApp1\Extensions";
            var pluginFiles = Directory.GetFiles(extensionsPath, "*.dll");

            var loaders = (
                from file in pluginFiles
                let asm = Assembly.LoadFile(file)
                from type in asm.GetTypes()
                where typeof(ILoader).IsAssignableFrom(type)
                select (ILoader)Activator.CreateInstance(type)
                ).ToArray();

            foreach (var loader in loaders)
            {
                Console.WriteLine("{0}", loader.Message());
            }
            Console.ReadLine();
        }
    }
}

