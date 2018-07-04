using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarWarsApp;
using System.Collections.Generic;

namespace StarWarsAppTesting
{
    [TestClass]
    public class StarWarsAppTesting
    {

        /// <summary>
        /// Test if API code works
        /// </summary>
        [TestMethod]
        public void SWAPI_getApiTest()
        {
            StarWarsResupplyManager target = new StarWarsResupplyManager();

            List<StarShips> ships = target.getStarWarsData();

            Assert.IsTrue(ships.Count != 0, "API code works correct");
        }

        /// <summary>
        /// Test if the calculation is correct passing Days 
        /// </summary>
        [TestMethod]
        public void calculate_consumable_in_hour_for_day()
        {
            StarWarsResupplyManager target = new StarWarsResupplyManager();
            string shipSpeed = "105"; //MGLT for ship as STRING (API)
            string shipConsumables = "5 days"; // consumable ship

            Assert.IsTrue(target.calulateConsumableInHour(shipConsumables, shipSpeed) == 12600, "Calulcation for DAYs work correctly");
        }

        /// <summary>
        /// Test if the calculation is correct passing Weeks 
        /// </summary>
        [TestMethod]
        public void calculate_consumable_in_hour_for_week()
        {
            StarWarsResupplyManager target = new StarWarsResupplyManager();
            string shipSpeed = "40"; //MGLT for ship as STRING (API)
            string shipConsumables = "3 weeks"; // consumable ship

            Assert.IsTrue(target.calulateConsumableInHour(shipConsumables, shipSpeed) == 20160, "Calulcation for WEEKs work correctly");
        }

        /// <summary>
        /// Test if the calculation is correct passing Months 
        /// </summary>
        [TestMethod]
        public void calculate_consumable_in_hour_for_months()
        {
            StarWarsResupplyManager target = new StarWarsResupplyManager();
            string shipSpeed = "75"; //MGLT for ship as STRING (API)
            string shipConsumables = "1 month"; // consumable ship

            Assert.IsTrue(target.calulateConsumableInHour(shipConsumables, shipSpeed) == 55125, "Calulcation for MONTHs work correctly");
        }


        /// <summary>
        /// Test if the calculation is correct passing Years 
        /// </summary>
        [TestMethod]
        public void calculate_consumable_in_hour_for_years()
        {
            StarWarsResupplyManager target = new StarWarsResupplyManager();
            string shipSpeed = "120"; //MGLT for ship as STRING (API)
            string shipConsumables = "2 years"; // consumable ship

            Assert.IsTrue(target.calulateConsumableInHour(shipConsumables, shipSpeed) == 2103840, "Calulcation for YEARs work correctly");

        }


        /// <summary>
        /// Test if the calculation is correct passing Years 
        /// </summary>
        [TestMethod]
        public void calculate_stops_for_a_ship()
        {
            StarWarsResupplyManager target = new StarWarsResupplyManager();
            long totalDistance = 1000000; //Distance to alculate
            long ConsumableInHour = 55125; //ConsumableInHour
          

            Assert.IsTrue(target.calcStops(totalDistance, ConsumableInHour) == 18, "Calulcation stops quantity");

        }
    }
}
