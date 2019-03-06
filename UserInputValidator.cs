using System.Collections;

namespace TictactoeVer2
{
    public class UserInputValidator
    {
        public bool IsValidInput(string input)
        {
            var coordinates = input.Split(',');
            
            if (IsPlayerQuitting(input) || coordinates.Length == 2 && 
                int.TryParse(coordinates[0], out var row) &&
                int.TryParse(coordinates[1], out var column) && 
                row > 0 && row < 4 && 
                column > 0 && column < 4)
            {
                return true;
            }
            
            return false;
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