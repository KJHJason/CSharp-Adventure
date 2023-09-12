using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Menu
{
    class ApplicationContext
    {
        public Dictionary<string, DrinkInfo> DrinksInv;
        public Dictionary<string, int> CartTable;

        public ApplicationContext()
        {
            CartTable = new Dictionary<string, int>();
            DrinksInv = new Dictionary<string, DrinkInfo>
            {
                {"IM", new DrinkInfo { Description = "Iced Milo", Price = 1.5, Quantity = 30 }},
                {"HM", new DrinkInfo { Description = "Hot Milo", Price = 1.2, Quantity = 40 }},
                {"IC", new DrinkInfo { Description = "Iced Coffee", Price = 1.5, Quantity = 50 }},
                {"HC", new DrinkInfo { Description = "Hot Coffee", Price = 1.2, Quantity = 20 }},
                {"1P", new DrinkInfo { Description = "100 Plus", Price = 1.1, Quantity = 1 }},
                {"CC", new DrinkInfo { Description = "Coca Cola", Price = 1.3, Quantity = 5 }}
            };
        }
    }
}
