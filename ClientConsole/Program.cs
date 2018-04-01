using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static VelibClient.Service1Client velibClient;
        static string selectedCity = null;
        static void Main(string[] args)
        {
            string commands = "Type the name of your station\n" +
                              "Type 'cities' for a list of the cities\n" + 
                              "Type 'city' to select a city\n" +
                              "Type 'help' for help\n" +
                              "Type 'exit' to exit\n";
                              
            
            Console.WriteLine("Welcome !");
            Console.WriteLine(commands);

            velibClient = new VelibClient.Service1Client();
            List<VelibClient.City> cities;

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
                    case "cities":
                        cities = velibClient.GetCities().ToList();
                        foreach (VelibClient.City city in cities)
                        {
                            Console.WriteLine(city.name + " - " + city.country_code);
                        }
                        break;
                    case "city":
                        selectedCity = null;
                        Console.WriteLine("Type a city name");
                        while (selectedCity == null)
                        {
                            string cityInput = Console.ReadLine();
                            switch (cityInput)
                            {
                                case "help":
                                    Console.WriteLine("Type a city name");
                                    break;
                               
                                default:
                                    cities = velibClient.GetCities().ToList();
                                    foreach (VelibClient.City c in cities)
                                    {
                                        if (c.name.ToLower().Equals(cityInput.ToLower()))
                                        {
                                            selectedCity = c.name;
                                            break;
                                        }
                                    }
                                    if (selectedCity == null)
                                        Console.WriteLine("This city doesn't exist");
                                    break;

                            }
                        }
                        Console.WriteLine("Type 'stations' for a list of the stations");
                        Console.WriteLine("Type the name of a station to display the numbers of bikes avalaible");
                        
                        break;
                    case "stations":
                        if (selectedCity != null)
                        {
                            List<VelibClient.Station> stations = velibClient.GetStations(selectedCity).ToList();
                            foreach (VelibClient.Station station in stations)
                            {
                                Console.WriteLine(station.name);
                            }

                        }
                        else
                            Console.WriteLine("Please select a city");
                        break;
                    default:
                        if (selectedCity != null)
                            Console.WriteLine(velibClient.GetStationData(input, selectedCity).ToString());
                        else
                            Console.WriteLine("Please select a city");
                        break;
                }
                
            }
            
            
        }

       
    }
}
