using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveValidator : MonoBehaviour
{
    [Inject] private BoardManager _boardManager;

    public bool IsMoveValid(Cell sourceCell, Cell targetCell, PieceType pieceType)
    {
        return IsWithinBounds(targetCell) && IsMoveDiagonal(sourceCell, targetCell) &&
               IsMoveForward(pieceType, sourceCell, targetCell) && IsCellEmpty(targetCell);
    }

    public Cell CanCapture(Cell sourceCell, PieceType pieceType, (int rowOffset, int colOffset) direction)
    {
        int fromRow = sourceCell.GetRow(), fromCol = sourceCell.GetColumn();

        if (_boardManager.IsWithinBounds(fromRow + direction.rowOffset, fromCol + direction.colOffset))
        {
            Cell opponentCell = _boardManager.GetCell(fromRow + direction.rowOffset, fromCol + direction.colOffset);

            if (opponentCell != null && IsOpponentPiece(opponentCell, pieceType))
            {
                if (_boardManager.IsWithinBounds(fromRow + 2 * direction.rowOffset, fromCol + 2 * direction.colOffset))
                {
                    Cell captureCell = _boardManager.GetCell(fromRow + 2 * direction.rowOffset, fromCol + 2 * direction.colOffset);
                    return IsCellEmpty(captureCell) ? captureCell : null;
                }
            }
        }
        return null;
    }

    private bool IsWithinBounds(Cell cell) => _boardManager.IsWithinBounds(cell.GetRow(), cell.GetColumn());
    private bool IsMoveDiagonal(Cell sourceCell, Cell targetCell) => Mathf.Abs(targetCell.GetRow() - sourceCell.GetRow()) == Mathf.Abs(targetCell.GetColumn() - sourceCell.GetColumn());
    private bool IsMoveForward(PieceType pieceType, Cell sourceCell, Cell targetCell) => (targetCell.GetRow() - sourceCell.GetRow()) * ((pieceType == PieceType.White) ? -1 : 1) > 0;
    private bool IsCellEmpty(Cell cell) => _boardManager.GetPieceTypeInCell(cell.GetRow(), cell.GetColumn()) == PieceType.Null;
    private bool IsOpponentPiece(Cell opponentCell, PieceType pieceType) => _boardManager.GetPieceTypeInCell(opponentCell.GetRow(), opponentCell.GetColumn()) != pieceType && _boardManager.GetPieceTypeInCell(opponentCell.GetRow(), opponentCell.GetColumn()) != PieceType.Null;

    public static IEnumerable<(int rowOffset, int colOffset)> GetMovementDirections(PieceType pieceType)
    {
        int direction = pieceType == PieceType.White ? -1 : 1;
        yield return (direction, 1);
        yield return (direction, -1);
    }
}
