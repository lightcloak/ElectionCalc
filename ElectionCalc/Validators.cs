using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalc
{
    static class Validators
    {
        // funkcja pochodzi z adresu: http://www.extensionmethod.net/2004/csharp/string/isvalidnip-isvalidregon-isvalidpesel
        public static bool IsValidPESEL(string input)
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

        // funkcja pochodzi z adresu: http://www.extensionmethod.net/2004/csharp/string/isvalidnip-isvalidregon-isvalidpesel
        private static int CalculateControlSum(string input, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(input[i].ToString());
            }
            return controlSum;
        }

        public static bool IsOldEnough(string input)
        {
            bool result = false;
            // Pobieramy numer pesel w celu otrzymania roku, miesiaca, dnia
            string yearmonthday = new string(input.Take(6).ToArray());
            int year = int.Parse(yearmonthday.Substring(0, 2));
            int month = int.Parse(yearmonthday.Substring(2, 2));
            int day = int.Parse(yearmonthday.Substring(4, 2));

            // Malo wydajna obsluga roku >= 2000
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
            // Malo eleganckie wyzerowanie referencji na obiekt
            // Singleton bylby tutaj lepszym wyjsciem
            age = null;
            return result;
        }
    }
}
