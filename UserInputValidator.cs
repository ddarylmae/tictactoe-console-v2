using System;
using System.Collections;

namespace TictactoeVer2
{
    public class UserInputValidator
    {
//        public bool IsValidInput(string input)
//        {
//            return IsPlayerQuitting(input) || IsValidCoordinate(input);
//        }

//        private bool IsValidCoordinate(string input)
//        {
//            var inputElements = input.Split(',');
//
//            if (!IsRowColumnPair(inputElements))
//            {
//                return false;
//            }
//
//            var row = inputElements[0];
//            var column = inputElements[1];
//            
//            return IsElementANumberAndWithinRange(row) && 
//                   IsElementANumberAndWithinRange(column);
//        }

//        private bool IsElementANumberAndWithinRange(string element)
//        {
//            try
//            {
//                var numValue = Convert.ToInt32(element);
//                return numValue > 0 && numValue < 4;
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("Exception in converting");
//                return false;
//            }
//        }
//
//        private bool IsRowColumnPair(string[] elements)
//        {
//            return elements.Length == 2;
//        }

        private bool IsPlayerQuitting(string input)
        {
            return input == "q";
        }

        public bool HasUserQuit(string input)
        {
            return input == "q";
        }
    }
}