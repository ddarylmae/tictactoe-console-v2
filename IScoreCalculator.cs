namespace TictactoeVer2
{
    public interface IScoreCalculator
    {
        int GetPossiblePointsFromBoard(IGameBoard board, Player currentPlayer);
    }

    public interface IGameBoard
    {
        // fillCoordinate, isCoordinateFilled
        FillResult FillSpecCoordinate(Move move);
        // checkIfBoardIsFilled
        bool IsFilled();
        // getValueFromRowAndColumn
        Player GetPlayerAt(int row, int column);
        // getSideLength
        int GetSideLength();
    }

    public enum FillResult
    {
        Successful, Taken
    }
}