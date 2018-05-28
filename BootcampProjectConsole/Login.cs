using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BootcampProjectConsole
{
    class Login
    {
        
        private static string GenerateSHA256String(string inputString)
        {


            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
        public string CredentialInput()
        {
            Console.WriteLine(@"
       __          ________ _      _____ ____  __  __ ______   _______ ____          
       \ \        / /  ____| |    / ____/ __ \|  \/  |  ____| |__   __/ __ \         
        \ \  /\  / /| |__  | |   | |   | |  | | \  / | |__       | | | |  | |        
         \ \/  \/ / |  __| | |   | |   | |  | | |\/| |  __|      | | | |  | |        
          \  /\  /  | |____| |___| |___| |__| | |  | | |____     | | | |__| |        
           \/  \/   |______|______\_____\____/|_|  |_|______|____|_|  \____/         
                   |  _ \ / __ \ / ____|__   __|/\   | \ | |_   _|                   
                   | |_) | |  | | (___    | |  /  \  |  \| | | |                     
                   |  _ <| |  | |\___ \   | | / /\ \ | . ` | | |                     
                   | |_) | |__| |____) |  | |/ ____ \| |\  |_| |_                    
  _____ _   _ _____|____/_\____/|_____/ _ |_/_/    \_\_| \_|_____|        _   _ _  __
 |_   _| \ | |__   __|  ____|  __ \| \ | |   /\   | |      |  _ \   /\   | \ | | |/ /
   | | |  \| |  | |  | |__  | |__) |  \| |  /  \  | |      | |_) | /  \  |  \| | ' / 
   | | | . ` |  | |  |  __| |  _  /| . ` | / /\ \ | |      |  _ < / /\ \ | . ` |  <  
  _| |_| |\  |  | |  | |____| | \ \| |\  |/ ____ \| |____  | |_) / ____ \| |\  | . \ 
 |_____|_| \_|  |_|  |______|_|  \_\_| \_/_/    \_\______| |____/_/    \_\_| \_|_|\_\
                ");
            System.Threading.Thread.Sleep(3000);
            var connectionQuery = new DataClassDataContext();
            try
            {
                var checkConnectivity = (from k in connectionQuery.users                                        
                                        select k).Count();
                if (checkConnectivity > 0)
                {
                    Console.WriteLine("Database Connected");
                    System.Threading.Thread.Sleep(1000);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }       
            Console.Clear();

            string username = String.Empty;
            bool successBoolLogon = false;
            try
            {
                int counter = 3;
                do
                {
                    var credentialQuery = new DataClassDataContext();
                    Console.WriteLine("Please insert your username");
                    username = Console.ReadLine();
                    Console.WriteLine("Please insert your password");
                    string password = String.Empty;
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    //Replaces keys on screen with *
                    while (info.Key != ConsoleKey.Enter)
                    {
                        if (info.Key != ConsoleKey.Backspace)
                        {
                            Console.Write("*");
                            password += info.KeyChar;
                        }
                        else if (info.Key == ConsoleKey.Backspace)
                        {
                            if (!string.IsNullOrEmpty(password))
                            {
                                password = password.Substring(0, password.Length - 1);
                                int pos = Console.CursorLeft;
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                                Console.Write(" ");
                                Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            }
                        }
                        info = Console.ReadKey(true);
                    }
                    Console.WriteLine();
                    password = GenerateSHA256String(password);
                    //Checks credentials to database for validity
                    var checkCredentials = (from k in credentialQuery.users
                                            where (k.username == username) && (k.password == password)
                                            select k).Count();

                    if (checkCredentials > 0)
                    {
                        Console.WriteLine("Successfull Login");
                        successBoolLogon = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Unsuccessfull Login - " + (counter - 1) + " more tries");
                    }
                    counter--;
                } while ((successBoolLogon == false) && (counter > 0));
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Clear();
            }

            if (successBoolLogon == false)
            {
                Console.WriteLine("Unsuccessfull Login - BYE!!!");
                username = String.Empty;
                Environment.Exit(0);
            }
            return username;
        }
    }
}
