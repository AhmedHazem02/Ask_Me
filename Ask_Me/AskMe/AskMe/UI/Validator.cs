using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMe.UI
{
    public class Validator
    {
        public static T convert<T>(string input)
        {
            bool valid = false;
            string userInput;
            while (!valid)
            {
                userInput = Utinity.GetUserInput(input);
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        return (T)converter.ConvertFromString(userInput);
                    }
                    else return default;
                }
                catch
                {
                    Utinity.PrintMsg("Invalid input .  Try Again. ", false);
                }
            }

            return default;
        }
    }
}
