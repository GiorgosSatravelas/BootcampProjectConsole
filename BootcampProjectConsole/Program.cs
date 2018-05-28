using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BootcampProjectConsole
{
    class Program
    {
        //Crypto the database
        //private static void CryptoDatabase()
        //{
        //    var connect = new DataClassDataContext();
        //    var userId = from l in connect.users  select l;
            
        //    foreach (var k in userId)
        //    {
        //        k.password = GenerateSHA256String(k.password);
        //    }

        //    try
        //    {
        //        connect.SubmitChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);

        //    }
        //}

        //private static string GenerateSHA256String(string inputString)
        //{


        //    SHA256 sha256 = SHA256Managed.Create();
        //    byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        //    byte[] hash = sha256.ComputeHash(bytes);
        //    return GetStringFromHash(hash);
        //}
        //private static string GetStringFromHash(byte[] hash)
        //{
        //    StringBuilder result = new StringBuilder();
        //    for (int i = 0; i < hash.Length; i++)
        //    {
        //        result.Append(hash[i].ToString("X2"));
        //    }
        //    return result.ToString();
        //}
        
        //Main Program

        public static List<string> userList = new List<string>();
        static void Main(string[] args)
        {
            //CryptoDatabase();
            var newLogin = new Login();
            string username = newLogin.CredentialInput();
            if (username == "admin")
            {
                var administrator = new Admin(username);
            }
            else
            {
                var simpleUser = new SimpleUser(username);
            }

        }
    }
}
