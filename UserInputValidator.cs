using System.Collections;

namespace TictactoeVer2
{
    public class UserInputValidator
    {
        public bool IsValidInput(string input)
        {
            return IsPlayerQuitting(input) || IsValidCoordinate(input);
        }

        private bool IsValidCoordinate(string input)
        {
            var coordinates = input.Split(',');

            if (coordinates.Length != 2)
            {
                return false;
            }

            var row = coordinates[0];
            var column = coordinates[1];
            
            return IsRowColumnPair(coordinates) && 
                   IsElementANumberAndWithinRange(row) &&
                   IsElementANumberAndWithinRange(column);
        }

        private bool IsElementANumberAndWithinRange(string element)
        {
            return int.TryParse(element, out var number) && number > 0 && number < 4;
        }

        private bool IsRowColumnPair(string[] coordinates)
        {
            return coordinates.Length == 2;
        }

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