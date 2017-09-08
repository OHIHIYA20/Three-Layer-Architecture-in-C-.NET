using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            Console.WriteLine("Create user = \"Test user\"");
            var user = new User()
            {
                Id = 1,
                Name = "Test user"
            };
            
            Console.WriteLine("Insert user");
            IUserDAL UserDal = new UserDAL();
            UserDal.Insert(user);

            Console.WriteLine("Refresh Db");
            UserDal.Refresh();

            Console.WriteLine("Get user from Db");
            var getUser = UserDal.GetById(1);
            Console.WriteLine("Get User = " + getUser.Name);

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
