using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warhammer40KDatacardGenerator
{
    static class ConsoleTools
    {
        public static string GetInput(string _message = "")
        {
        if(_message != "")
                Console.WriteLine(_message);
            return Console.ReadLine();
        }

        public static bool AskYesNo(string _message)
        {
            string input = GetInput(_message).ToLower();
            
            while (input != "y" && input != "n")
            {
                Console.WriteLine("Please input a y or n (case-insensitive)");
                input = GetInput(_message).ToLower();
            }

            return input == "y";
        }

        public static bool GetLineIfNotEmpty(string _message, out string _userInput)
        {
            _userInput = GetInput(_message);
            return _userInput != "";
        }
    }
}
