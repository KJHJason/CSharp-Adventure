using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Menu;
using VendingMachine.Inputs;

namespace VendingMachine
{
    class Vendor : Common
    {
        private VendorMenu VendorMenu;

        public Vendor(ApplicationContext applicationContext)
        {
            AppContext = applicationContext;
            DrinkMenu = new DrinkMenu(AppContext);
            VendorMenu = new VendorMenu(AppContext);
        }

        public override void Main()
        {
            int vendorChoice = -1;
            while (vendorChoice != 0)
            {
                VendorMenu.PrintMenu();
                vendorChoice = IntInp.Prompt("Enter choice: ");
                switch (vendorChoice)
                {
                    case 0:
                        Console.WriteLine("You have exited the vendor's menu.\n");
                        break;
                    case 1:
                        AddDrink();
                        break;
                    case 2:
                        ReplenishDrinks();
                        break;
                    case 3:
                        DrinkMenu.PrintInventory();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        public void AddDrink()
        {
            string drinkId = "";
            while (drinkId != "0")
            {
                drinkId = StrInp.Prompt("Enter drink ID (0 to cancel): ").ToUpper();
                if (drinkId == "0")
                {
                    Console.WriteLine("Adding of new drink has been cancelled.");
                    continue;
                }

                drinkId = StrInp.RemoveWhitespace(drinkId);
                if (AppContext.DrinksInv.ContainsKey(drinkId))
                {
                    Console.WriteLine("Drink ID already exists!");
                    continue;
                }

                string drinkDesc = StrInp.Prompt("Enter description of drink: ");
                double drinkPrice = DouInp.Prompt("Enter price: $");
                int drinkQty = IntInp.Prompt("Enter quantity: ");

                DrinkInfo newDrink = new DrinkInfo
                {
                    Description = drinkDesc,
                    Price = drinkPrice,
                    Quantity = drinkQty
                };

                AppContext.DrinksInv[drinkId] = newDrink;
                Console.WriteLine("Drinks added!\n");
            }
        }

        public void ReplenishDrinks()
        {
            string drinkId = "";
            while (drinkId != "0")
            {
                DrinkMenu.PrintInventory();
                drinkId = StrInp.Prompt("\nEnter drink ID (0 to cancel): ").ToUpper();
                if (drinkId == "0")
                {
                    Console.WriteLine("Drink replenishment has been cancelled.\n");
                    continue;
                }

                DrinkInfo drinkInfo;
                drinkId = StrInp.RemoveWhitespace(drinkId);
                if (!AppContext.DrinksInv.TryGetValue(drinkId, out drinkInfo))
                {
                    Console.WriteLine("Drink ID does not exists!\n");
                    continue;
                }

                if (drinkInfo.Quantity > 5)
                {
                    // Part of the assignment's requirements
                    Console.WriteLine("No need to replenish. Quantity is greater than 5.\n");
                    continue;
                }

                int drinkQty = IntInp.Prompt("Enter quantity: ", "Error in entered quantity!");
                DrinkMenu.UpdateQty(drinkId, drinkInfo.Quantity + drinkQty);
            }
        }
    }
}
