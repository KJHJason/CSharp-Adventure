using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Menu
{
    struct DrinkInfo
    {
        public string Description;
        public double Price;
        public int Quantity;
    }

    abstract class Menu
    {
        public ApplicationContext AppContext { get; set; }

        public Menu(ApplicationContext menuContext)
        {
            AppContext = menuContext;
        }

        public abstract void PrintMenu();
    }
}
