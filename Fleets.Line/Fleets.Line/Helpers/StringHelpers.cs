using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fleets.Line.Helpers
{
    /// <summary>
    /// StringHelpers
    /// </summary>
    public static class StringHelpers
    {
        public static int GetDigitsInString(string value)
        {
            return Convert.ToInt32(String.Join("", value.Where(char.IsDigit)));
        }

        public static string GetCharactersInString(string value)
        {
            return String.Join("", value.Where(char.IsLetter));
        }

        public static double ResolveValue(string value)
        {
            double resolveValue = 0;
            if(double.TryParse(value, out resolveValue))
            {
                return resolveValue;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}