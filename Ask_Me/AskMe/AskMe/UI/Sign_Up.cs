using AskMe.Context;
using AskMe.Context.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.UI
{
    public class Sign_Up
    {
        public static void Sign()
        {
            // Sign Up And Add User In DataBase
            UserAccount account = new UserAccount();
            account.Name=Validator.convert<string>("Plz Enter Your Name");
            account.Username = Validator.convert<string>("Plz Enter Valid User Name");
            using Data_Context ctx = new Data_Context();
            var res = ctx.Users.Any(U=>U.Username== account.Username);              
            while(res==true) {
                Utinity.PrintMsg("This User Not Valid", false);
                account.Username = Validator.convert<string>("Plz Enter Valid User Name");
                res = ctx.Users.Any(U => U.Username == account.Username);
            }
            account.Password = Validator.convert<string>("Plz Enter Your Password");
            account.Email= Validator.convert<string>("Plz Enter Your Email");
            account.Age = Validator.convert<int>("Plz Enter Your Age");
            account.Country = Validator.convert<string>("Plz Enter Your Country");
            account.Is_Unkown = Validator.convert<bool>("Plz Enter true  If You Want To Be Unknown else Enter false");                    
                ctx.Users.Add(account);
                ctx.SaveChanges();
            ctx.Dispose();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SucessFull Sign Up ");
            Console.ResetColor();
            Utinity.PressEnter();

        }
    }
}
