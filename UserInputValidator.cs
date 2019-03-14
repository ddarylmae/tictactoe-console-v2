namespace TictactoeVer2
{
    public class UserInputValidator
    {
        public bool IsBoardSizeValid(string choice)
        {
            return int.TryParse(choice, out var size) && size > 2 && size < 11;
        }
    }
}