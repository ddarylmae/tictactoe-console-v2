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
            return IsRowColumnPair(coordinates) && 
                   IsRowANumber(coordinates, out var row) &&
                   int.TryParse(coordinates[1], out var column) && 
                   row > 0 && row < 4 && 
                   column > 0 && column < 4;
        }

        private static bool IsRowANumber(string[] coordinates, out int row)
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