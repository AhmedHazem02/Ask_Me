using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.UI
{
    public class Utinity
    {
        public static string GetUserInput(string prompt)
        {
            Console.WriteLine($"{prompt}");
            return Console.ReadLine();
        }
        public static void PressEnter()
        {
            Console.WriteLine("\nPlease Enter To Continue");
            Console.ReadLine();
        }
        public static void PrintMsg(string msg, bool succ = true) // Red Or Yello
        {
            if (succ) Console.ForegroundColor = ConsoleColor.Yellow;
            else Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
            PressEnter();
        }
        public static void Print_Dot(int Time = 10)
        {

            for (int i = 0; i < Time; i++)
            {
                Console.Write('.');
                Thread.Sleep(200); // For Waiting
            }
            Console.Clear();
        }
    }
}
