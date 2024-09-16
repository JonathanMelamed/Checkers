using System.Collections.Generic;

public static class DirectionProvider
{
    public static IEnumerable<(int rowOffset, int colOffset)> GetMovementDirections(PieceType pieceType)
    {
        // if (_boardManager.IsQueen(pieceType))
        // {
        //     yield return (1, 1);
        //     yield return (1, -1);
        //     yield return (-1, 1);
        //     yield return (-1, -1);
        // }
        int direction = (pieceType == PieceType.White) ? -1 : 1;
        yield return (direction, 1);
        yield return (direction, -1);
    }
}
