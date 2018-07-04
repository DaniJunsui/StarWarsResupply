using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StarWarsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Welcoem message
            Console.WriteLine("\n\t\t+----------------------------------------------------------------+");
            Console.WriteLine("\t\t|    *                     .            *                   .    |");
            Console.WriteLine("\t\t|                                               .                |");
            Console.WriteLine("\t\t|          Welcome to the Star Wars Resupply calculator          |");
            Console.WriteLine("\t\t|        *            .                                     *    |");
            Console.WriteLine("\t\t|    .                        *                           .      |");
            Console.WriteLine("\t\t+----------------------------------------------------------------+");

            Console.WriteLine("\t\t\nLoading Star Wars database...");


            //Get the Manager instance
            StarWarsResupplyManager starWarsManager = new StarWarsResupplyManager();

            //Read Data from API
            List<StarShips> allShips = starWarsManager.getStarWarsData();

            //If there was an error, the app will clsoe
            if (allShips.Count < 1)
            {
                Console.WriteLine("\n\n\nNO DATA FOUND\nCloseing application...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("\nShips loaded!");
            }

            //Run the APP
            starWarsManager.appMenu(allShips);
        }
    }

}
