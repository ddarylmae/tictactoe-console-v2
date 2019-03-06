using System.Collections;

namespace TictactoeVer2
{
    public class UserInputValidator
    {
        public bool IsValidInput(string input)
        {
            if (IsPlayerQuitting(input) || IsValidCoordinate(input))
            {
                return true;
            }
            
            return false;
        }

        private static bool IsValidCoordinate(string input)
        {
            var coordinates = input.Split(',');
            return coordinates.Length == 2 && 
                   int.TryParse(coordinates[0], out var row) &&
                   int.TryParse(coordinates[1], out var column) && 
                   row > 0 && row < 4 && 
                   column > 0 && column < 4;
        }

        private static bool IsPlayerQuitting(string input)
        {
            return input == "q";
        }


        public bool HasUserQuit(string input)
        {
            return input == "q";
        }
    }
}