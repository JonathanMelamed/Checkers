using System.Collections.Generic;
using UnityEngine;

public class MoveValidator : MonoBehaviour
{
    [SerializeField] private BoardManager _boardManager;

    public bool IsMoveValid(Cell sourceCell, Cell targetCell, PieceType pieceType)
    {
        return IsWithinBounds(targetCell) &&
               IsMoveDiagonal(sourceCell, targetCell) &&
               IsMoveForward(pieceType, sourceCell, targetCell) &&
               IsCellEmpty(targetCell);
    }

    public Cell CanCapture(Cell sourceCell, PieceType pieceType, (int rowOffset, int colOffset) direction)
    {
        int fromRow = sourceCell.GetRow();
        int fromCol = sourceCell.GetColumn();
        Cell opponentCell = _boardManager.GetCell(fromRow + direction.rowOffset, fromCol + direction.colOffset);
        if (opponentCell != null && IsOpponentPiece(opponentCell, pieceType))
        {
            Cell captureCell = _boardManager.GetCell(fromRow + 2 * direction.rowOffset, fromCol + 2 * direction.colOffset);
            if (captureCell != null && IsCellEmpty(captureCell))
            {
                return captureCell;
            }
        }

        return null;
    }

    private bool IsWithinBounds(Cell cell)
    {
        return _boardManager.IsWithinBounds(cell.GetRow(), cell.GetColumn());
    }

    private bool IsMoveDiagonal(Cell sourceCell, Cell targetCell)
    {
        int rowDifference = Mathf.Abs(targetCell.GetRow() - sourceCell.GetRow());
        int colDifference = Mathf.Abs(targetCell.GetColumn() - sourceCell.GetColumn());
        return rowDifference == colDifference;
    }

    private bool IsMoveForward(PieceType pieceType, Cell sourceCell, Cell targetCell)
    {
        int direction = (pieceType == PieceType.White) ? -1 : 1;
        return (targetCell.GetRow() - sourceCell.GetRow()) * direction > 0;
    }

    private bool IsCellEmpty(Cell cell)
    {
        return _boardManager.GetPieceTypeInCell(cell.GetRow(), cell.GetColumn()) == PieceType.Null;
    }

    private bool IsOpponentPiece(Cell opponentCell, PieceType pieceType)
    {
        PieceType opponentPieceType = _boardManager.GetPieceTypeInCell(opponentCell.GetRow(), opponentCell.GetColumn());
        return opponentPieceType != PieceType.Null && opponentPieceType != pieceType;
    }

    public IEnumerable<(int rowOffset, int colOffset)> GetMovementDirections(PieceType pieceType)
    {
        if (_boardManager.IsQueen(pieceType))
        {
            yield return (1, 1);
            yield return (1, -1);
            yield return (-1, 1);
            yield return (-1, -1);
        }
        else
        {
            int direction = (pieceType == PieceType.White) ? -1 : 1;
            yield return (direction, 1);
            yield return (direction, -1);
        }
    }
}
