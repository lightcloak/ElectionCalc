using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalc
{
    static class Validators
    {
        // Source: http://www.extensionmethod.net/2004/csharp/string/isvalidnip-isvalidregon-isvalidpesel
        public static bool ValidPESEL(string input)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            bool result = false;
            if (input.Length == 11)
            {
                int controlSum = CalculateControlSum(input, weights);
                int controlNum = controlSum % 10;
                controlNum = 10 - controlNum;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }
                int lastDigit = int.Parse(input[input.Length - 1].ToString());
                result = controlNum == lastDigit;
            }
            return result;
        }

        // Source: http://www.extensionmethod.net/2004/csharp/string/isvalidnip-isvalidregon-isvalidpesel
        private static int CalculateControlSum(string input, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(input[i].ToString());
            }
            return controlSum;
        }

        public static bool OldEnough(string input)
        {
            bool result = false;
            // Take PESEL to extract day, mont, year of birth
            string yearmonthday = new string(input.Take(6).ToArray());
            int year = int.Parse(yearmonthday.Substring(0, 2));
            int month = int.Parse(yearmonthday.Substring(2, 2));
            int day = int.Parse(yearmonthday.Substring(4, 2));

            // Not so good way of handling year >= 2000
            if (month > 20)
            {
                month = int.Parse((month - 20).ToString("D2"));
                year = year + 2000;
            }
            else
            {
                year = year + 1900;
            }

            DateTime birth = new DateTime(year, month, day);

            Age age = new Age(birth);

            if (age.Years >= 18)
            {
                result = true;
            }

            age = null;
            return result;
        }
    }
}
