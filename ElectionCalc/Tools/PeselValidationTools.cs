namespace ElectionCalc
{ 
    using System;
    using System.Linq;

    static class PeselValidationTools
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

        public static bool OldEnough(long value)
        {
            bool result = false;
            string input = value.ToString();

            // long is a value type so no need for string.IsNullOrWhiteSpace(input)
            if (input.Length != 11) return result;

            // Process PESEL to extract day, mont, year of birth
            // Take would be nicer
            int year = int.Parse(input.Substring(0, 2));
            int month = int.Parse(input.Substring(2, 2));
            int day = int.Parse(input.Substring(4, 2));

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
