namespace TictactoeVer2
{
    public class UserInputParser
    {
        private const int DefaultBoardSize = 3;
        
        public bool TryParseBoardSize(string input, out int size)
        {
            if (IsInputBlank(input))
            {
                size = DefaultBoardSize;
                return true;
            }
            
            return int.TryParse(input, out size) && IsWithinRange(size);
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

        private bool IsRowColumnPairValidAndWithinRange(string[] elements, int boardSize, out int row, out int col)
        {
            var rowStringValue = elements[0];
            var columnStringValue = elements[1];
            col = 0;

            return IsElementANumberAndWithinRange(rowStringValue, boardSize, out row) &&
                   IsElementANumberAndWithinRange(columnStringValue, boardSize, out col);
        }

        private bool IsElementANumberAndWithinRange(string element, int boardSize, out int number)
        {
            return int.TryParse(element, out number) && number > 0 && number <= boardSize;
        }

        public bool TryParseMove(string input, int boardSize, out Move move)
        {
            var elements = input.Split(',');
            move = null;
            if (IsRowColumnPair(elements) && IsRowColumnPairValidAndWithinRange(elements, boardSize, out int row, out int column))
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
    }
}