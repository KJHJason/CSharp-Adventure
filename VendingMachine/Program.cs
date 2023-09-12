using System;
using VendingMachine.Menu;
using VendingMachine.Inputs;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var appContext = new ApplicationContext();
            var customerLogic = new Customer(appContext);
            var vendorLogic = new Vendor(appContext);

            while (true)
            {
                bool isAVendor = StrInp.PromptBinary("Are you a vendor (y/N)?: ", "Y", "N", "N");
                if (isAVendor)
                {
                    vendorLogic.Main();
                }
                else
                {
                    customerLogic.Main();
                }
            }
        }
    }
}
