using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampProjectConsole
{
    class Admin:User
    {
        public Admin(string Username) : base(Username)
        {
            AdminMenu();

        }
        private void AdminMenu()
        {
            short currentItem = 0, c;
            ConsoleKeyInfo key;
            string[] menuItems = { "View Cooperative's internal bank account ", "View Members' bank accounts ",
                "Deposit to Member's bank account ", "Withdraw from Member's bank account ","Print today's transactions to a file",
                "Exit the application " };
            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Welcome "+username+", use the arrows to navigate and press Enter\n");
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
                    ViewMemberAccount(username);
                    break;
                case 2:
                    DepositMemberAccount(username);
                    break;
                case 3:
                    WithdrawMemberAccount(username);
                    break;
                case 4:
                    PrintTransactions(username);
                    break;
                case 5:
                    Exit();
                    break;
            }
            AdminMenu();



        }
    }
}
