namespace TictactoeVer2
{
    public interface IScoreCalculator
    {
        int GetPossiblePointsFromBoard(char[] board, Player currentPlayer);
    }
}