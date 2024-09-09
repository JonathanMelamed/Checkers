using System.Collections.Generic;
using UnityEngine;

public class MoveValidator : MonoBehaviour
{
    private BoardManager _boardManager;

    public MoveValidator(BoardManager boardManager)
    {
        _boardManager = boardManager;
    }

    public bool IsCellEmpty(int row, int column)
    {
        return _boardManager.GetPieceTypeInCell(row, column) == PieceType.Null;
    }

    public bool IsMoveForward(Piece piece, int currentRow, int targetRow)
    {
        if (piece.PieceType == PieceType.White)
            return targetRow > currentRow; // White moves down
        else if (piece.PieceType == PieceType.Black)
            return targetRow < currentRow; // Black moves up
        return false;
    }

    public bool CanCapture(int fromRow, int fromCol, int targetRow, int targetCol, PieceType currentPieceType)
    {
        PieceType targetPieceType = _boardManager.GetPieceTypeInCell(targetRow, targetCol);

        if (targetPieceType != PieceType.Null && targetPieceType != currentPieceType)
        {
            return true;
        }

        return false;
    }

    // Add other rules as needed, like diagonal movement, etc.
}