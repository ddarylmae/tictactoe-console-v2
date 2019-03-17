namespace TictactoeVer2
{
    public class UserInputValidator
    {
        public bool TryParseBoardSize(string input, out int size)
        {
            SetInputToDefaultSizeIfBlank(ref input);
            return int.TryParse(input, out size) && IsWithinRange(size);
        }

        private void SetInputToDefaultSizeIfBlank(ref string input)
        {
            input = input.Trim() == "" ? "3" : input;
        }

        private bool IsWithinRange(int size)
        {
            return size > 2 && size < 11;
        }
    }
}