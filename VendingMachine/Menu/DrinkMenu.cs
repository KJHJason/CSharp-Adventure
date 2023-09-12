using System;
using System.Collections.Generic;
using System.Text;


namespace VendingMachine.Menu
{
    class DrinkMenu : Menu
    {
        public DrinkMenu(ApplicationContext menuContext) : base(menuContext)
        {
        }

        public void DecrementQty(string drinkId)
        {
            DrinkInfo drinkInfo = AppContext.DrinksInv[drinkId];
            drinkInfo.Quantity--;
            AppContext.DrinksInv[drinkId] = drinkInfo;
        }

        public void UpdateQty(string drinkId, int qty)
        {
            DrinkInfo drinkInfo = AppContext.DrinksInv[drinkId];
            drinkInfo.Quantity = qty;
            AppContext.DrinksInv[drinkId] = drinkInfo;
        }

        private static string formatDrinkId(string drinkId)
        {
            return drinkId + ".";
        }

        private static string formatPrice(double price)
        {
            return $"(${price:F2})";
        }

        public void GetFormattedInfo(string drinkId, DrinkInfo drinkInfo,
            out string drinkFmtId, out string drinkDesc, out string drinkPrice)
        {
            drinkFmtId = formatDrinkId(drinkId);

            drinkDesc = drinkInfo.Description;

            double price = drinkInfo.Price;
            drinkPrice = formatPrice(price);
        }

        // Combines parts of a message without the qty
        public string CombineParts(string drinkId, string drinkDesc, string drinkPrice)
        {
            string[] words = { drinkId, drinkDesc, drinkPrice };
            return string.Join(" ", words);
        }

        private int calculateMenuOffset()
        {
            int maxLength = 0;
            foreach (KeyValuePair<string, DrinkInfo> kvp in AppContext.DrinksInv)
            {
                string drinkId, drinkDesc, fmtPrice;
                GetFormattedInfo(kvp.Key, kvp.Value, 
                    out drinkId, out drinkDesc, out fmtPrice);

                string combinedStr = CombineParts(drinkId, drinkDesc, fmtPrice);
                if (combinedStr.Length > maxLength)
                {
                    maxLength = combinedStr.Length;
                }
            }
            return maxLength;
        }

        public static string FormatQty(int qty)
        {
            if (qty > 0)
            {
                return $"Qty: {qty.ToString()}";
            }
            return "***out of stock***";
        }

        public static void PrintWithPadding(int offset, string s, string qty)
        {
            int offsetDiff = offset - s.Length;

            string paddedStr = s + new string(' ', offsetDiff + 1);
            Console.WriteLine(paddedStr + qty);
        }

        private void menuLogic()
        {
            int offset = calculateMenuOffset();
            foreach (KeyValuePair<string, DrinkInfo> kvp in AppContext.DrinksInv)
            {
                string drinkId, drinkDesc, fmtPrice;
                GetFormattedInfo(kvp.Key, kvp.Value,
                    out drinkId, out drinkDesc, out fmtPrice);

                string qty = FormatQty(kvp.Value.Quantity);

                string combinedStr = CombineParts(drinkId, drinkDesc, fmtPrice);

                PrintWithPadding(offset, combinedStr, qty);
            }
        }

        public override void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine(@"Welcome to ABC Vending Machine.
Select from following choices to continue:");
            menuLogic();
            Console.WriteLine("0. Exit / Payment");
        }

        public void PrintInventory()
        {
            menuLogic();
        }

        public bool ValidateItemInCart(string drinkId, out DrinkInfo drinkInfo)
        {
            return AppContext.DrinksInv.TryGetValue(drinkId, out drinkInfo);
        }

        public void PrintCart()
        {
            int offset = calculateMenuOffset();
            HashSet<string> invalid = new HashSet<string>();
            foreach (KeyValuePair<string, int> kvp in AppContext.CartTable)
            {
                string drinkId = kvp.Key;
                int inCart = kvp.Value;
                DrinkInfo drinkInfo;
                if (!ValidateItemInCart(drinkId, out drinkInfo))
                {
                    invalid.Add(drinkId);
                    continue;
                }

                string drinkFmtId, drinkDesc, drinkPrice;
                GetFormattedInfo(kvp.Key, drinkInfo,
                    out drinkFmtId, out drinkDesc, out drinkPrice);


                // Should always be > 0
                string qty = FormatQty(inCart);
                string combinedStr = CombineParts(formatDrinkId(drinkId), drinkDesc, drinkPrice);
                PrintWithPadding(offset, combinedStr, qty);
            }
        }
    }
}
