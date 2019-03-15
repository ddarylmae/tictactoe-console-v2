namespace TictactoeVer2
{
    public class UserInputValidator
    {
        public bool IsBoardSizeValid(string input)
        {
            var size = ParseBoardSize(input);
            
            return IsWithinRange(size);
        }

        private int ParseBoardSize(string choice)
        {
            if(int.TryParse(choice, out var size))
            {
                return size;
            }
            return -1;
        }

        private bool IsWithinRange(int size)
        {
            return size > 2 && size < 11;
        }
    }
}