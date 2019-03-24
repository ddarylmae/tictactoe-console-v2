namespace TictactoeVer2
{
    public interface IScoreCalculator
    {
        int GetPossiblePointsFromBoard(Player currentPlayer);
    }
}