using System;

namespace Core.System.Security
{
    public  static class Generator
    {
        public static string Generate6DigitNumber()
        {
            Random random = new Random();
            return random.Next(0, 1000000).ToString("D6");
        }
    }
}
