using System.Collections.Generic;

public interface IMoveService
{
    bool IsMoveValid(Cell sourceCell, Cell targetCell, PieceType pieceType);
    Cell CanCapture(Cell sourceCell, PieceType pieceType, (int rowOffset, int colOffset) direction);
    IEnumerable<(int rowOffset, int colOffset)> GetMovementDirections(PieceType pieceType);
}