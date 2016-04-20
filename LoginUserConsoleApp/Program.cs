using LoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginUserConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Hacker;Integrated Security=True";
            Console.WriteLine("Hello manager. This insures the safety of your program. Only authorized individuals have access to add another emplyee to the system");
            Console.WriteLine();
            Console.WriteLine("Please enter the individuals user name : ");
            Console.WriteLine("(Press enter to submit)");
            Console.ReadKey(true);
            string username = Console.ReadLine();
            Console.WriteLine("Please enter the individuals password");
            string password = Console.ReadLine();
            var mgr = new UserManager(ConnectionString);
            mgr.AddUser(username, password);
            Console.WriteLine("Your new user " + username + " has been created");
            Console.ReadKey(true);
        }
    }
}
