namespace TictactoeVer2
{
    public interface IScoreCalculator
    {
        int CalculatePoints(IGameBoard board, Player currentPlayer);
    }

    public interface IGameBoard
    {
        // fillCoordinate, isCoordinateFilled
        FillResult FillCoordinate(Move move);
        // checkIfBoardIsFilled
        bool IsFilled();
        // getValueFromRowAndColumn
        Player GetPlayerAt(int row, int column);
        // getSideLength
        int GetSideLength();
        string GetFormatted();
    }

    public enum FillResult
    {
        Successful, Taken
    }
}