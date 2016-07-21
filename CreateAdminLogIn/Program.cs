using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Data;

namespace CreateAdminLogIn
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository repo = new UserRepository(Properties.Settings.Default.ConStr);
            Console.WriteLine("Select a username");
            string userName = Console.ReadLine();
            Console.WriteLine("Type your password");
            string passWord = Console.ReadLine();
            Console.WriteLine("Reenter your password");
            string retype = Console.ReadLine();
            while(passWord != retype)
            {
                Console.WriteLine("Reenter your password");
                retype = Console.ReadLine();
            }
            repo.AddUser(userName, passWord);
            Console.WriteLine("User Created");
            Console.ReadKey(true);
        }
    }
}
