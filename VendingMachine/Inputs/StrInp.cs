using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Inputs
{
    class StrInp
    {
        // From https://stackoverflow.com/a/30732794/16377492
        public static string RemoveWhitespace(string inp)
        {
            return string.Join("", inp.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

        }

        public static string Prompt(string prompt, string errorMsg)
        {
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine().Trim();
                if (userInput.Length > 0)
                {
                    return userInput;
                }
                Console.WriteLine(errorMsg);
                Console.WriteLine();
            }
        }

        public static string Prompt(string prompt)
        {
            return Prompt(prompt, "You cannot leave this empty!");
        }

        public static bool PromptBinary(string prompt, string trueCond, string falseCond, string defaultOp)
        {
            if (defaultOp == "")
            {
                throw new ArgumentException("defaultOp cannot be empty");
            }

            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine().Trim().ToUpper();
                if (userInput.Length == 0)
                {
                    return defaultOp == trueCond;
                }

                if (userInput != trueCond && userInput != falseCond)
                {
                    Console.WriteLine($"Please enter either \"{trueCond}\" or \"{falseCond}\"!");
                }
                else
                {
                    return userInput == trueCond;
                }
            }
        }
    }
}
