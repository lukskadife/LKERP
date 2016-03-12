using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {            

            ServiceReference1.EntegrasyonClient client = new ServiceReference1.EntegrasyonClient();

            Console.WriteLine(client.MalzemeleriEntegreEt());
            Console.ReadLine();
        }
    }
}
