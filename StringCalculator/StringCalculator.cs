using System.Collections.ObjectModel;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        /// <summary>
        ///     Sum up to 3 comma concatenated int numbers
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int Add(string numbers)
        {
            var negativeNumbers = new Collection<int>();

            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            var result = AddNumbers(numbers, ref negativeNumbers);

            if(negativeNumbers.Any())
                throw new NegativeNotAllowedException($"negatives not allowed: {string.Join(',',negativeNumbers)}");

            return result;
        }

        /// <summary>
        /// Sum numbers
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="negativeNumbers"></param>
        /// <returns></returns>
        private static int AddNumbers(string numbers, ref Collection<int> negativeNumbers)
        {
            if (int.TryParse(numbers, out var number))
                return CheckNumber(number, ref negativeNumbers);

            // first char on single line defines separator
            var newLineSeparated = numbers.Split("\n");
            var numberString = numbers.Replace("\n", ",");
            if (newLineSeparated.Length > 1)
                if (!int.TryParse(newLineSeparated[0], out var num))
                    numberString = numberString.Replace(newLineSeparated[0], ",");

            var numberList = numberString.Split(',');
            var sum = 0;
            foreach (var stringNumber in numberList)
                if (int.TryParse(stringNumber, out var num))
                    sum += CheckNumber(num, ref negativeNumbers);

            return sum;
        }

        /// <summary>
        ///     Throws Exception if number is negative
        /// </summary>
        /// <param name="number"></param>
        /// <exception cref="NegativeNotAllowedException">if number is negative</exception>
        /// <returns></returns>
        private static int CheckNumber(int number, ref Collection<int> negativeNumbers)
        {
            if (number < 0)
                negativeNumbers.Add(number);

            return number;
        }
    }
}