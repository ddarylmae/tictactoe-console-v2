namespace TictactoeVer2
{
    public class UserInputHandler
    {
        private const int DefaultBoardSize = 3;
        public int CurrentBoardSize;
        
        public bool TryParseBoardSize(string input, out int size)
        {
            if (IsInputBlank(input))
            {
                size = DefaultBoardSize;
                return true;
            }
            
            return int.TryParse(input, out size) && IsWithinRange(size);
        }
        
        public bool TryParseMove(string input, out Move move)
        {
            var elements = input.Split(',');
            move = null;
            if (IsRowColumnPair(elements) && IsRowColumnPairValidAndWithinRange(elements, out int row, out int column))
            {
                move = new Move
                {
                    Row = row,
                    Column = column
                };
                return true;
            }
            return false;
        }
        
        private bool IsInputBlank(string input)
        {
            return input.Trim() == "";
        }

        private bool IsWithinRange(int size)
        {
            return size > 2 && size < 11;
        }
        
        private static bool IsRowColumnPair(string[] inputElements)
        {
            return inputElements.Length == 2;
        }

        private bool IsRowColumnPairValidAndWithinRange(string[] elements, out int row, out int col)
        {
            var rowStringValue = elements[0];
            var columnStringValue = elements[1];
            col = 0;

            return IsElementANumberAndWithinRange(rowStringValue, out row) &&
                   IsElementANumberAndWithinRange(columnStringValue, out col);
        }

        private bool IsElementANumberAndWithinRange(string element, out int number)
        {
            return int.TryParse(element, out number) && number > 0 && number <= CurrentBoardSize;
        }
    }
}