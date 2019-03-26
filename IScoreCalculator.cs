namespace TictactoeVer2
{
    public interface IScoreCalculator
    {
        int CalculatePoints(IGameBoard board, Player currentPlayer);
    }
}