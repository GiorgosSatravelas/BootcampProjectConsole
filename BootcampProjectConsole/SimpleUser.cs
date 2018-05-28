using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampProjectConsole
{
    class SimpleUser:User
    {
        public SimpleUser(string Username):base(Username)
        {
            UserMenu();
        }
        private void UserMenu()
        {
            short currentItem = 0, c;
            ConsoleKeyInfo key;
            string[] menuItems = { "View your bank account ", "Deposit to Cooperative's internal bank account ",
                "Deposit to Member's bank account ", "Print today's transactions to a file",
                "Exit the application " };
            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Welcome " + username + ", use the arrows to navigate and press Enter\n");
                Console.ResetColor();
                for (c = 0; c < menuItems.Length; c++)
                {
                    if (currentItem == c)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuItems[c]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuItems[c]);
                    }
                }
                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    currentItem++;
                    if (currentItem > menuItems.Length - 1) currentItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    currentItem--;
                    if (currentItem < 0) currentItem = Convert.ToInt16(menuItems.Length - 1);
                }
            } while (key.KeyChar != 13);
            switch (currentItem)
            {
                case 0:
                    ViewYourAccount(username);
                    break;
                case 1:
                    DepositAdminAccount(username);
                    break;
                case 2:
                    DepositMemberAccount(username);
                    break;
                case 3:
                    PrintTransactions(username);
                    break;
                case 4:
                    Exit();
                    break;                
            }
            UserMenu();
        }
    }
}
