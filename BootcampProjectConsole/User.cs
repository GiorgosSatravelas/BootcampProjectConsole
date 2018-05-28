using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampProjectConsole
{
    abstract class User
    {
        protected string username; 
        public User(string Username)
        {
            username=Username;
            var amountQuery = new DataClassDataContext();          
        }
        protected void ViewYourAccount(string username)
        {
            var connect = new DataClassDataContext();
            int userId = (from l in connect.users where (l.username == username) select l.id).First();
            var userAmount = (from k in connect.accounts
                              where k.user_id == userId
                              select k.amount).First();
            Console.Clear();
            Console.WriteLine("Your amount is: "+ userAmount);
            Console.WriteLine("Press enter to go back");
            Console.ReadLine();

        }
        
        protected void ViewMemberAccount(string username)
        {
            var connect = new DataClassDataContext();
            var menuList = new List<string>();
            var menuQuery = from d in connect.users
                            where d.username!=username
                            select d.username;

            menuList.AddRange(menuQuery.Distinct());
            short currentItem = 0, c;
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                for (c = 0; c < menuList.Count; c++)
                {
                    if (currentItem == c)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuList[c]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuList[c]);
                    }
                }
                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    currentItem++;
                    if (currentItem > menuList.Count - 1) currentItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    currentItem--;
                    if (currentItem < 0) currentItem = Convert.ToInt16(menuList.Count - 1);
                }
            } while (key.KeyChar != 13);

            int userId = (from l in connect.users where (l.username == menuList[currentItem]) select l.id).First();
            var userAmount = (from k in connect.accounts
                      where k.user_id == userId
                      select k.amount).First();
            Console.Clear();
            Console.WriteLine("The amount of user "+menuList[currentItem]+ " is: "+userAmount);
            Console.WriteLine("Press enter to go back");
            Console.ReadLine();
        }
        protected void DepositMemberAccount(string username)
        {
            Console.WriteLine("Please Choose the Member account you want to deposit:");
            //gets the list of the members and their according balance
            var connect = new DataClassDataContext();
            var menuList = new List<string>();
            var menuQuery = from d in connect.users
                            where d.username != username
                            select d.username;

            menuList.AddRange(menuQuery.Distinct());
            short currentItem = 0, c;
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                for (c = 0; c < menuList.Count; c++)
                {
                    if (currentItem == c)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuList[c]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuList[c]);
                    }
                }
                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    currentItem++;
                    if (currentItem > menuList.Count - 1) currentItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    currentItem--;
                    if (currentItem < 0) currentItem = Convert.ToInt16(menuList.Count - 1);
                }
            } while (key.KeyChar != 13);

            int userId = (from l in connect.users where (l.username == menuList[currentItem]) select l.id).First();
            var userAmount = (from k in connect.accounts
                              where k.user_id == userId
                              select k.amount).First();
            Console.Clear();
            Console.WriteLine("Please enter the amount you want to Deposit:");
            decimal deposition = 0;
            bool isDecimal = false;
            do
            {

                string input = Console.ReadLine();

                if (decimal.TryParse(input, out deposition))
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a decimal");
                    isDecimal = true;
                }


            } while (isDecimal);

            int currentUserId= (from l in connect.users where l.username == username select l.id).First();
            var balance = (from k in connect.accounts
                           where k.user_id == currentUserId
                           select k.amount).First();
            
            while (deposition > balance)
            {
                Console.Clear();
                Console.WriteLine("Your balance is: " + balance);
                Console.WriteLine("Please choose a smaller number!");
                isDecimal = false;
                do
                {

                    string input = Console.ReadLine();

                    if (decimal.TryParse(input, out deposition))
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a decimal");
                        isDecimal = true;
                    }


                } while (isDecimal);

            }

            var Transfer = new BankAccount(menuList[currentItem],username,deposition,"deposit");
            Transfer.Transaction();


            Console.Clear();
        }
        protected void WithdrawMemberAccount(string username)
        {
            Console.WriteLine("Please Choose the Member account you want to withdraw:");
            //gets the list of the members and their according balance
            var connect = new DataClassDataContext();
            var menuList = new List<string>();
            var menuQuery = from d in connect.users
                            where d.username != username
                            select d.username;

            menuList.AddRange(menuQuery.Distinct());
            short currentItem = 0, c;
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                for (c = 0; c < menuList.Count; c++)
                {
                    if (currentItem == c)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuList[c]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuList[c]);
                    }
                }
                key = Console.ReadKey(true);
                if (key.Key.ToString() == "DownArrow")
                {
                    currentItem++;
                    if (currentItem > menuList.Count - 1) currentItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    currentItem--;
                    if (currentItem < 0) currentItem = Convert.ToInt16(menuList.Count - 1);
                }
            } while (key.KeyChar != 13);

            int userId = (from l in connect.users where (l.username == menuList[currentItem]) select l.id).First();
            var userAmount = (from k in connect.accounts
                              where k.user_id == userId
                              select k.amount).First();
            Console.Clear();
            Console.WriteLine("Please enter the amount you want to Withdraw:");
            decimal withdrawl = 0;
            bool isDecimal = false;
            do
            {

                string input = Console.ReadLine();

                if (decimal.TryParse(input, out withdrawl))
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a decimal");
                    isDecimal = true;
                }


            } while (isDecimal);
            int currentUserId = (from l in connect.users where l.username == username select l.id).First();
            var balance = (from k in connect.accounts
                           where k.user_id == currentUserId
                           select k.amount).First();

            while (withdrawl > userAmount)
            {
                Console.Clear();
                Console.WriteLine("User's balance is: " + userAmount);
                Console.WriteLine("Please choose a smaller number!");
                isDecimal = false;
                do
                {

                    string input = Console.ReadLine();

                    if (decimal.TryParse(input, out withdrawl))
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a decimal");
                        isDecimal = true;
                    }


                } while (isDecimal);


            }
            var Transfer = new BankAccount(username, menuList[currentItem], withdrawl,"withdrawl");
            Transfer.Transaction();

        }
        protected void PrintTransactions(string username)
        {
            var transactionPrint = new BankAccount(username);
            transactionPrint.PrintBankTransactions(username);

        }
        protected void DepositAdminAccount(string username)
        {
            Console.Clear();
            var connect = new DataClassDataContext();
            int userId = (from l in connect.users where (l.username == "admin") select l.id).First();
            var userAmount = (from k in connect.accounts
                              where k.user_id == userId
                              select k.amount).First();
            Console.Clear();
            Console.WriteLine("Please enter the amount you want to Deposit:");
            decimal deposition = 0;
            bool isDecimal = false;
            do
            {

                string input = Console.ReadLine();

                if (decimal.TryParse(input, out deposition))
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a decimal");
                    isDecimal = true;
                }


            } while (isDecimal);
            int currentUserId = (from l in connect.users where l.username == username select l.id).First();
            var balance = (from k in connect.accounts
                           where k.user_id == currentUserId
                           select k.amount).First();

            while (deposition > balance)
            {
                Console.Clear();
                Console.WriteLine("Your balance is: " + balance);
                Console.WriteLine("Please choose a smaller number!");
                deposition = Convert.ToDecimal(Console.ReadLine());

            }
            var Transfer = new BankAccount("admin", username, deposition, "deposit");
            Transfer.Transaction();
            Console.Clear();
        }

        protected void Exit()
        {
            Console.Clear();
            Console.WriteLine("Thanks for using our bank, see you again!!!");
            Environment.Exit(0);
        }

    }
}
