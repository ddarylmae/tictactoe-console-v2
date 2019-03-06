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

            var IsRowANum = int.TryParse(coordinates[0], out var rowVal);
            
            var IsColANum = int.TryParse(coordinates[1], out var colVal);
            
            return IsRowColumnPair(coordinates) && 
                   IsRowANum &&
                   IsColANum && 
                   IsRowWithinRange(rowVal) && 
                   IsColumnWithinRange(colVal);
        }

        private bool IsColumnWithinRange(int column)
        {
            return column > 0 && column < 4;
        }   
        
        private bool IsRowWithinRange(int row)
        {
            return row > 0 && row < 4;
        }

        private bool IsColumnANumber(string[] coordinates, out int column)
        {
            return int.TryParse(coordinates[1], out column);
        }

        private bool IsRowANumber(string[] coordinates, out int row)
        {
            return int.TryParse(coordinates[0], out row);
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