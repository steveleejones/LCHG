using System;
using System.Linq;
using System.Text;

namespace LCHG.Utilities
{
    public class PackageCodeGenerator
    {
        private const string CHAR_LIST = "0123456789abcdefghijklmnopqrstuvwxyz";

        public static string GeneratePackageCode(int propertyReferenceId, int duration, DateTime deptDate, int mealBasisId, int departureAirportId, int adults, int children, int totalPrice)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Encode(propertyReferenceId, 4));
            sb.Append(Encode(duration, 1));
            sb.Append(Encode((((deptDate.Year % 3 * 12) - 1) + deptDate.Month), 1));
            sb.Append(Encode(deptDate.Day, 1));
            sb.Append(Encode(mealBasisId, 1));
            sb.Append(Encode(departureAirportId, 2));
            sb.Append(Encode(adults * 5 + children, 1));
            sb.Append(Encode((totalPrice), 4)); //Removing the times 100 as will do that before here

            return sb.ToString();
        }

        private static String Encode(int input, int maxLength)
        {

            string valueItem = Base36.NumberToBase36(input);

            if (valueItem.Length != maxLength)
            {
                if (valueItem.Length < maxLength)
                    valueItem = valueItem.PadLeft(maxLength, '0');
                else
                    valueItem = valueItem.Remove(maxLength);
            }

            return valueItem;
        }

        private static Int64 Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            long result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += CHAR_LIST.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }
    }
}
