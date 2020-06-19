using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dan_XXXV_Bojana_Backo
{
    public class Console_Messages
    {
        public static int NumberOfParticipients()
        {
            uint participients = 0;
            bool repeat;
            do
            {
                Console.Write("\nPlease enter the number of participients -> ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nYou must provide some input!");
                    repeat = true;
                }
                else if (!UInt32.TryParse(input, out participients))
                {
                    Console.WriteLine("\nRead instructions!!!!");
                    repeat = true;
                }
                else
                {
                    participients = UInt32.Parse(input);
                    repeat = false;
                }
            } while (repeat);
            return (int)participients;
        }
        public static int NumberToGuess()
        {
            uint numberToGuess = 0;
            bool repeat;
            do
            {
                Console.Write("\nPlease enter the guessing number from 1 to 100  -> ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nYou must provide some input!");
                    repeat = true;
                }
                else if (!UInt32.TryParse(input, out numberToGuess))
                {
                    Console.WriteLine("\nRead instructions!!!!");
                    repeat = true;
                }
                else if (UInt32.Parse(input) > 100 || UInt32.Parse(input) < 1)
                {
                    Console.WriteLine("\nRead instructions!!!!");
                    repeat = true;
                }
                else
                {
                    numberToGuess = UInt32.Parse(input);
                    repeat = false;
                }
            } while (repeat);
            return (int)numberToGuess;
        }
    }
}
