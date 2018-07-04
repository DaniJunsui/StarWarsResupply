using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApp
{
   public class StarShipsAllData
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<StarShips> Results { get; set; }
    }
}
