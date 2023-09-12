using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Menu;
using VendingMachine.Inputs;

namespace VendingMachine
{
    class Customer : Common
    {
        public Customer(ApplicationContext applicationContext)
        {
            AppContext = applicationContext;
            DrinkMenu = new DrinkMenu(AppContext);
        }

        public override void Main()
        {
            MenuSelection();
            Purchase();
        }

        public void MenuSelection()
        {
            int totalDrinks = 0;
            double totalAmt = 0;
            string choice = "";
            while (choice != "0")
            {
                DrinkMenu.PrintMenu();
                choice = StrInp.Prompt("Enter choice: ").ToUpper();
                if (choice == "0")
                {
                    continue;
                }

                DrinkInfo drinkInfo;
                if (!AppContext.DrinksInv.TryGetValue(choice, out drinkInfo))
                {
                    Console.WriteLine("Your selected choice is not in the list.");
                    Console.WriteLine();
                    continue;
                }

                int qty = drinkInfo.Quantity;
                string drinkName = drinkInfo.Description;
                if (qty <= 0)
                {
                    Console.WriteLine($"{drinkName} is out of stock");
                    continue;
                }

                if (AppContext.CartTable.ContainsKey(choice))
                {
                    AppContext.CartTable[choice]++;
                }
                else
                {
                    AppContext.CartTable[choice] = 1;
                }

                totalDrinks++;
                totalAmt += drinkInfo.Price;
                DrinkMenu.DecrementQty(choice);
                Console.WriteLine($"No. of drinks selected = {totalDrinks}");
            }

            if (totalAmt <= 0)
            {
                Console.WriteLine("Thank you and have a nice day!\n");
                return;
            }
        }

        public double calculateTotalInCart()
        {
            double totalAmt = 0;
            foreach (KeyValuePair<string, int> kvp in AppContext.CartTable)
            {
                string drinkId = kvp.Key;
                int amt = kvp.Value;

                DrinkInfo drinkInfo = AppContext.DrinksInv[drinkId];
                totalAmt += drinkInfo.Price * amt;
            }

            return totalAmt;
        }

        static int PromptNoOfNotes(int dollar)
        {
            return IntInp.Prompt($"Enter no. of ${dollar} notes: ");
        }

        public void Purchase()
        {
            var totalAmt = calculateTotalInCart();
            if (totalAmt <= 0)
            {
                return;
            }

            double paid = 0;
            string cancelPurchaseInp = "";
            while (cancelPurchaseInp != "X" && paid < totalAmt)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Paid: ${paid:N2}");
                Console.WriteLine($"Please pay ${totalAmt:N2} for the following drinks:");
                DrinkMenu.PrintCart();
                Console.WriteLine("\nPlease indicate your payment:");
                Console.WriteLine("1. Insert $10 notes");
                Console.WriteLine("2. Insert $5 notes");
                Console.WriteLine("3. Insert $2 notes");
                Console.WriteLine("X. Cancel Purchase");

                cancelPurchaseInp = StrInp.Prompt("Enter payment option: ").ToUpper();
                if (cancelPurchaseInp == "X")
                {
                    foreach (KeyValuePair<string, int> kvp in AppContext.CartTable)
                    {
                        string drinkId = kvp.Key;
                        int amt = kvp.Value;

                        DrinkInfo drinkInfo = AppContext.DrinksInv[drinkId];
                        drinkInfo.Quantity += amt;
                        AppContext.DrinksInv[drinkId] = drinkInfo;
                    }
                    AppContext.CartTable = new Dictionary<string, int>();

                    if (paid > 0)
                    {
                        Console.WriteLine("Not enough to pay for the drinks");
                        Console.WriteLine("Take back your cash!");
                    }
                    Console.WriteLine("Purchase is cancelled. Thank you.\n\n");
                }
                else
                {
                    switch (cancelPurchaseInp)
                    {
                        case "1":
                            paid += PromptNoOfNotes(10) * 10;
                            break;
                        case "2":
                            paid += PromptNoOfNotes(5) * 5;
                            break;
                        case "3":
                            paid += PromptNoOfNotes(2) * 2;
                            break;
                        default:
                            Console.WriteLine("Invalid payment option!");
                            break;
                    }
                }
            }

            if (cancelPurchaseInp != "X")
            {
                double change = paid - totalAmt;
                if (change > 0)
                {
                    Console.WriteLine($"Please collect your change: {change:N2}");
                }
                Console.WriteLine("Drinks paid. Thank you.");
            }
            return;
        }
    }
}
