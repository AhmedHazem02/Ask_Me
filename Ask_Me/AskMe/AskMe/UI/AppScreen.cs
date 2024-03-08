using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AskMe.UI
{
    public class AppScreen
    {
      
        public static int LoginPage()
        { 
            Console.WriteLine("-------------- Welcome To My Ask Me App ---------------");
            Console.WriteLine("1 : Sign Up");
            Console.WriteLine("2 : Login In");
            int input;
            do
            {
                input = Validator.convert<int>("Plz Enter 1 Or 2 Only");

            } while (input > 2 || input < 1);

            return input;

        }
        public static void Display_Menue()
        {
            Console.WriteLine("1: Print Question To Me");
            Console.WriteLine("2: Print Question From Me");
            Console.WriteLine("3: Answer Question");
            Console.WriteLine("4: Delete Question");
            Console.WriteLine("5: Ask Question");
            Console.WriteLine("6: Feeds");
            Console.WriteLine("7: List System Users");
            Console.WriteLine("8: LogOut");
        }
    }


}
