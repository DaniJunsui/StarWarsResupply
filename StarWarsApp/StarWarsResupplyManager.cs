using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StarWarsApp
{
    public class StarWarsResupplyManager
    {
        #region CONSTANTS

        //CONSTANTS
        const string API_RUL = "https://swapi.co/api/starships/";
        const int CALC_DAY = 24;
        const int CALC_WEEK = 168; // 24 * 7
        const int CALC_MONTH = 678; // 24 * 7 * 4
        const int CALC_YEAR = 8760; // 365 * 24

        #endregion


        #region API

        /// <summary>
        /// Get the Ship information from SWAPI
        /// </summary>
        /// <returns></returns>
        public List<StarShips> getStarWarsData()
        {
            List<StarShips> allShips = new List<StarShips>();

            string api = API_RUL;
            try
            {
                do
                {
                    // Create a request for the URL and get the response	
                    WebRequest request = WebRequest.Create(api);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream dataStream = response.GetResponseStream();

                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);

                    // Read the content and parse it to our Entity
                    StarShipsAllData allData = JsonConvert.DeserializeObject<StarShipsAllData>(reader.ReadToEnd());

                    //Get the next API URL
                    api = allData.Next;

                    //Add current range to our general List
                    allShips.AddRange(allData.Results);

                    //Close all readers
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                } while (api != null);

            }
            catch (Exception e)
            {
                printError(e.InnerException.ToString());
            }

            try
            {
                //Remove from the list all ships without MGLT information
                allShips.RemoveAll(s => s.MGLT == "unknown");

                //For each ship, we will calculate the consumable in Hours
                allShips = allShips.Select(x => { x.ConsumablesInHours = calulateConsumableInHour(x.Consumables, x.MGLT); return x; }).ToList();
            }
            catch(Exception e)
            {
                printError(e.InnerException.ToString());
            }

            return allShips;
        }

        /// <summary>
        /// Calculate the Consumable in hours
        /// </summary>
        /// <param name="shipConsumables"></param>
        /// <param name="shipSpeed"></param>
        /// <returns></returns>
        public long calulateConsumableInHour(string shipConsumables, string shipSpeed)
        {
            //First we have to convert the Consumables string to number of hours in INT. Based on (NUMBER -space- TIME_STRING)
            //Getting the numbers from the String
            int.TryParse(Regex.Match(shipConsumables, @"\d+").Value, out int consumableNumber);
            int.TryParse(shipSpeed, out int shipMGLT);

            //Start by 1 to avoid dividing by 0;
            long consumableTotal = 1;

            //Switching if the consumible contains DAYs, WEEKs, MONTHs or YEARs
            if (shipConsumables.ToUpper().Contains("DAY"))
            {
                consumableTotal = consumableNumber * (long)HoursEnumerator.CALC_DAY;
            }
            else if (shipConsumables.ToUpper().Contains("WEEK"))
            {
                consumableTotal = consumableNumber * (long)HoursEnumerator.CALC_WEEK;
            }
            else if (shipConsumables.ToUpper().Contains("MONTH"))
            {
                consumableTotal = consumableNumber * (long)HoursEnumerator.CALC_MONTH;
            }
            else if (shipConsumables.ToUpper().Contains("YEAR"))
            {
                consumableTotal = consumableNumber * (long)HoursEnumerator.CALC_YEAR; ;
            }

            return consumableTotal *= shipMGLT;
        }

        #endregion


        #region MENU

        /// <summary>
        /// The Main Menu from the console application
        /// </summary>
        /// <param name="allShips"></param>
        public void appMenu(List<StarShips> allShips)
        {
            bool exitApp = false;

            do
            {
                //Write the Menu and get the input
                Console.Write("\n\n\tINSERT DISTANCE FOR SHIPS (Press '0' for exit): ");
                string inputData = Console.ReadLine();
                long MGLTInput;

                //Test if the number is bigger than a LONG accepts
                if (inputData.Length < 19)
                {
                    //Parse the input and prove it is OK
                    if (long.TryParse(inputData, out MGLTInput))
                    {
                        //Check if the number is less than 0
                        if (MGLTInput > 0)
                        {
                            //If pressed 0, the APP will close
                            if (MGLTInput == 0)
                            {
                                exitApp = true;
                            }
                            else
                            {
                                //Paint the result
                                paintResult(MGLTInput, allShips);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\n\t -- Wrong input... Please... think about your request");
                        }
                    }
                    else
                    {
                        //The input was not correct
                        Console.WriteLine("\n\n\t -- Wrong input... Just numbers are allowed");
                    }
                }
                else
                {
                    Console.WriteLine("\n\n\t -- Wrong input... Number is to hight!");
                }


            } while (!exitApp);
        }

        /// <summary>
        /// Paints the result giving an input
        /// </summary>
        /// <param name="MGLTInput"></param>
        /// <param name="allShips"></param>
        private void paintResult(long MGLTInput, List<StarShips> allShips)
        {
            Console.Write("\n");
            Console.WriteLine("  -- Information for " + MGLTInput.ToString("#,#", CultureInfo.InvariantCulture) + " MGLT\n");
            
            //Calculate for each ship
            foreach (StarShips ship in allShips)
            {
                //Write the information about each ship
                Console.WriteLine("\t" + ship.Name + ": " + calcStops(MGLTInput, ship.ConsumablesInHours));
            }
        }


        #endregion

        #region Calulcation

        /// <summary>
        /// Calculates the Resupply count
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public double calcStops(long total, long ConsumableInHour)
        {
            try
            {
                //Return the division
                return (total / ConsumableInHour);
            }
            catch(Exception e)
            {
                printError(e.InnerException.ToString());
                return 0;
            }
        }


        /// <summary>
        /// Paint a error in the console passing a string
        /// </summary>
        /// <param name="message"></param>
        private void printError(string message)
        {
            Console.WriteLine("___________\n\tAn error happened.\n___________\n\t Error: " + message);
        }

        #endregion
    }

}


