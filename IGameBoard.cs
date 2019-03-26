namespace TictactoeVer2
{
    public interface IGameBoard
    {
        FillResult FillCoordinate(Move move);
        bool IsFilled();
        Player GetPlayerAt(int row, int column);
        int GetSideLength();
        string GetFormatted();
    }
}