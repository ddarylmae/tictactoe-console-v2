using System;
using System.Collections;

namespace TictactoeVer2
{
    public class UserInputValidator
    {
        public bool HasUserQuit(string input)
        {
            return input == "q";
        }
    }
}