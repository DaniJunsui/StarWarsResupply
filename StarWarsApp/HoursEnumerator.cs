using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApp
{
    public enum HoursEnumerator 
    {
        CALC_DAY = 24,
        CALC_WEEK = 168,  // 24 * 7
        CALC_MONTH = 735, // Average of hours from a complete year
        CALC_YEAR = 8766 // 365 * 24 + 6
    }
}
