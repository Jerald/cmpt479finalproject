using System;


namespace Stater.Utils
{
    // Text utils
    public static class t
    {
        // Prints out a number of spaces equal to the given tab level
        public static string tab(int tabLevel)
        {
            string output = "";

            for (int i = 0; i < tabLevel*4; i++)
            {
                output += " ";
            }

            return output;
        }

        public static void debugPrint(string body, string toBeEnclosed)
        {
            Console.WriteLine(body + "'" + toBeEnclosed + "'");
        }
    }
}