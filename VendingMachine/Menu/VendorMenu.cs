using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Menu
{
    class VendorMenu : Menu
    {
        private string Menu;

        public VendorMenu(ApplicationContext menuContext) : base(menuContext)
        {
            Menu = @"Welcome to ABC Vending Machine.
Select from following choices to continue:
1. Add Drink Type
2. Replenish Drink
3. Display Inventory
0. Exit
";
        }

        public override void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine(Menu);
        }
    }
}
