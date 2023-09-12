using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Menu;

namespace VendingMachine
{
    abstract class Common
    {
        public ApplicationContext AppContext;
        public DrinkMenu DrinkMenu;

        public abstract void Main();
    }
}
