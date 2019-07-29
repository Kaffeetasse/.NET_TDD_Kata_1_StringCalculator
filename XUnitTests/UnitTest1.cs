using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;
using StringCalculator;

namespace XUnitTests
{
    /**
     * Arrange, Act, Assert
     *
     * Naming your tests
     * name of the method being tested
     * scenario under which it's being tested
     * expected behavior when the scenario is invoked
     * sample: Add_SingleNumber_ReturnsSameNumber
     *
     */
    public class UnitTest1
    {
        private Random random;

        public UnitTest1()
        {
            random = new System.Random();
        }

        [Fact]
        public void Add_SingleNumber_ReturnsSameNumber()
        {
            const int expectedResult = 1;

            var result = StringCalculator.StringCalculator.Add("1");

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Add_NoNumber_ReturnsZero()
        {
            const int expectedResult = 0;

            var result = StringCalculator.StringCalculator.Add("");

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Add_TwoNumbers_ReturnsSum()
        {
            const int expectedResult = 4;

            var result = StringCalculator.StringCalculator.Add("1,3");

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Add_RandomAmountOfNumbers_ReturnsSum()
        {
            var countOfNumbers = random.Next(1, 100);
            var numbers = new Collection<int>();
            for (var i = 0; i < countOfNumbers; i++)
                numbers.Add(random.Next(1,1000));
            var stringNumbers = string.Join(",", numbers);
            var expectedResult = numbers.Sum();

            var result = StringCalculator.StringCalculator.Add(stringNumbers);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Add_AllowNewLineAsSeparator_ReturnsSum()
        {
            const int expectedResult = 6;
            const string testCase = "1\n2,3";

            var result = StringCalculator.StringCalculator.Add(testCase);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Add_AllowDefinedSeparator_ReturnsSum()
        {
            const int expectedResult = 3;
            const string testCase = ";\n1;2";

            var result = StringCalculator.StringCalculator.Add(testCase);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Add_NegativeNumber_ThrowNegativeNotAllowedException()
        {
            const string testCase = "-1";

            Assert.Throws<NegativeNotAllowedException>(() => StringCalculator.StringCalculator.Add(testCase));
        }

        [Fact]
        public void Add_NegativeNumberExceptionMessage_ThrowNegativeNotAllowedExceptionWithNumber()
        {
            const string testCase = "-1";
            void TestCode()
            {
                StringCalculator.StringCalculator.Add(testCase);
            }

            var ex = Record.Exception(TestCode);

            Assert.NotNull(ex);
            Assert.IsType<NegativeNotAllowedException>(ex);
            Assert.Equal("negatives not allowed: -1",ex.Message);
        }

        [Fact]
        public void Add_MultipleNegativeNumbers_NegativeNumbersInExceptionMessage()
        {
            const string testCase = "-1,2,-3";
            void TestCode()
            {
                StringCalculator.StringCalculator.Add(testCase);
            }

            var ex = Record.Exception(TestCode);

            Assert.NotNull(ex);
            Assert.IsType<NegativeNotAllowedException>(ex);
            Assert.Equal("negatives not allowed: -1,-3",ex.Message);
        }
    }
}
