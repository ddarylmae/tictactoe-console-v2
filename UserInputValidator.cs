namespace TictactoeVer2
{
    public class UserInputValidator
    {
        private int DefaultBoardSize = 3;
        
        public bool TryParseBoardSize(string input, out int size)
        {
            if (IsInputBlank(input))
            {
                size = DefaultBoardSize;
                return true;
            }
            
            return int.TryParse(input, out size) && IsWithinRange(size);
        }
        
        public bool IsInputBlank(string input)
        {
            return input.Trim() == "";
        }

        private bool IsWithinRange(int size)
        {
            return size > 2 && size < 11;
        }
    }
}