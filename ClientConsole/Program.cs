using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string commands = "Type the name of your station\n" +
                              "Type 'stations' for a list of the stations availables\n" +
                              "Type 'help' for help\n" +
                              "Type 'exit' to exit\n";
                              
            
            Console.WriteLine("Welcome !");
            Console.WriteLine(commands);

            VelibClient.Service1Client velibClient = new VelibClient.Service1Client();

            Boolean serve = true;
            while (serve)
            {
                
                string input = Console.ReadLine();

                switch(input)
                {
                    case "help":
                        Console.WriteLine(commands);
                        break;
                    case "exit":
                        serve = false;
                        break;
                    case "stations":
                        break;
                    default:
                        Console.WriteLine(velibClient.GetData(input));
                        break;
                }
                
            }
            
            
        }
    }
}
