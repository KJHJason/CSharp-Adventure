using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Inputs
{
    class DouInp
    {
        public static double Prompt(string prompt, string errorMsg)
        {
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine().Trim();
                if (userInput.Length <= 0)
                {
                    Console.WriteLine("You cannot leave this empty!");
                }

                try
                {
                    return double.Parse(userInput);
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is OverflowException)
                    {
                        Console.WriteLine(errorMsg);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public static double Prompt(string prompt)
        {
            return Prompt(prompt, "Please enter a valid decimal number!");
        }
    }
}
