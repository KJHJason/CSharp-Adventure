using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Inputs
{
    class IntInp
    {
        public static int Prompt(string prompt, string errorMsg)
        {
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine().Trim();
                if (userInput.Length <= 0)
                {
                    Console.WriteLine("You cannot leave this empty!");
                    continue;
                }

                try
                {
                    var parsed = Int32.Parse(userInput);
                    if (parsed < 0)
                    {
                        Console.WriteLine(errorMsg);
                    }
                    else
                    {
                        return parsed;
                    }
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

        public static int Prompt(string prompt)
        {
            return Prompt(prompt, "Please enter a valid number!");
        }
    }
}
