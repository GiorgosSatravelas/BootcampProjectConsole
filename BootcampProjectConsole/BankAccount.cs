using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;


namespace BootcampProjectConsole
{
    class BankAccount
    {
        private string DepositedUser;
        private string WithdrawlUser;
        private decimal TransactionAmount;
        private string TransactionType;
        private string UserPrinting;

        public BankAccount(string userPrinting)
        {
            UserPrinting = userPrinting;
        }
        public BankAccount(string depositedUser,string withdrawlUser,decimal transactionAmount, string transactionType)
        {
            DepositedUser = depositedUser;
            WithdrawlUser = withdrawlUser;
            TransactionAmount = transactionAmount;
            TransactionType = transactionType;
        }
        public void Transaction()
        {
            var connect = new DataClassDataContext();
            int userId = (from l in connect.users where (l.username == DepositedUser) select l.id).First();
            var userAmount = from k in connect.accounts
                             where k.user_id == userId
                             select k;
            foreach (var k in userAmount)
            {
                k.amount = k.amount + TransactionAmount;                
            }

            try
            {
                connect.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            userId = (from l in connect.users where (l.username == WithdrawlUser) select l.id).First();
            userAmount = from k in connect.accounts
                             where k.user_id == userId
                             select k;
            foreach (var k in userAmount)
            {
                k.amount = k.amount - TransactionAmount;
            }

            try
            {
                connect.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            
            Console.Clear();
            Console.WriteLine(ToString());
            Console.WriteLine("Press enter to go back");
            Console.ReadLine();
        }

        public override string ToString()
        {
            var Time= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFF");

            var cultureInfo = CultureInfo.GetCultureInfo("el-GR");
            var formattedAmount = String.Format(cultureInfo, "{0:C}", TransactionAmount);            

            if (TransactionType =="deposit")
            {
                string output= "User " + WithdrawlUser + " made a deposit to " + DepositedUser + " at: " + Time + " the amount of: " + formattedAmount;                
                Program.userList.Add(output);                
                return output;                
            }
            else
            {
                string output= "User " + DepositedUser + " made a withdraw from " +  WithdrawlUser + " at: " + Time + " the amount of: " + formattedAmount;                              
                Program.userList.Add(output);                
                return output;               
            }            
        }
        public void PrintBankTransactions(string username)
        {
            List<string> print = new List<string>();       
            print = Program.userList.ToList();
            string time = DateTime.Now.ToString("dd_MM_yyyy");
            string reverseTime = DateTime.Now.ToString("yyyy-MM-dd");
            string filename = "statement_" + username+"_" + time + ".txt";
            string path = @"C:\Users\Kostas\Desktop\BootcampProjectConsole\";
            string fullPath = path + filename;
            if (!File.Exists(fullPath))
            {
                using (StreamWriter outputFile = new StreamWriter(fullPath))
                {
                    foreach (string line in print)
                        if (line.Contains(reverseTime))
                        {
                            outputFile.WriteLine(line);
                        }
                }
            }
            else if (File.Exists(fullPath))
            {
                using (StreamWriter sw = File.AppendText(fullPath))
                {
                    foreach (string line in print)
                        if (line.Contains(reverseTime))
                        {
                            sw.WriteLine(line);
                        }
                }
            }            
            Console.Clear();
            Console.WriteLine("Transactions Printed - Press Enter to go back");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
