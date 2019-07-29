using System;

namespace StringCalculator
{
    public class NegativeNotAllowedException : Exception
    {
        public NegativeNotAllowedException()
        {
        }

        public NegativeNotAllowedException(string message) : base(message)
        {
        }
    }
}