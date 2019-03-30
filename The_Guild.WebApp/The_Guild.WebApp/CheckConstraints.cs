using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Guild.WebApp
{
    static class CheckConstraints
    {
        static int intMin = 0;
        static int intMax = 40;
        static public bool ValidInt(int? value)
        {
            if (value == null)
            {
                return true;
            }
            else if (value >= intMin && value <= intMax)
            {
                return true;
            }

            //else if value < 0
            return false;
        }

        static public bool NonNegativeDecimal(decimal? value)
        {
            if (value == null)
            {
                return true;
            }
            else if (value >= 0)
            {
                return true;
            }

            //else if value < 0
            return false;
        }
    }
}
