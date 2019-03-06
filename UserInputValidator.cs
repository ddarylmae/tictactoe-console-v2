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
            var colVal = coordinates[1];
            
            var IsColANumber = int.TryParse(coordinates[1], out var column);
            
            return IsRowColumnPair(coordinates) && 
                   IsElementANumberAndWithinRange(row) &&
                   IsColANumber && 
                   IsColumnWithinRange(column);
        }

        private bool IsElementANumberAndWithinRange(string element)
        {
            return int.TryParse(element, out var number) && number > 0 && number < 4;
        }

        private bool IsColumnWithinRange(int column)
        {
            return column > 0 && column < 4;
        }   
        
        private bool IsRowWithinRange(int row)
        {
            return row > 0 && row < 4;
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