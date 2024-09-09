using System.Collections.Generic;
using UnityEngine;

public class PieceMovementOptions : MonoBehaviour
{
    private MoveValidator _moveValidator;
    private BoardManager _boardManager;

    public PieceMovementOptions(BoardManager boardManager, MoveValidator moveValidator)
    {
        _boardManager = boardManager;
        _moveValidator = moveValidator;
    }

    
    public List<Cell> GetValidMoves(Cell sourceCell, Piece piece)
    {
        List<Cell> validCells = new List<Cell>();
        int currentRow = sourceCell.GetRow();
        int currentCol = sourceCell.GetColumn();

        int[] rowOffsets = { 1, -1 }; 
        int[] colOffsets = { 1, -1 }; 

        foreach (int rowOffset in rowOffsets)
        {
            foreach (int colOffset in colOffsets)
            {
                int targetRow = currentRow + rowOffset;
                int targetCol = currentCol + colOffset;

                if (_boardManager.IsWithinBounds(targetRow, targetCol) &&
                    _moveValidator.IsCellEmpty(targetRow, targetCol) &&
                    _moveValidator.IsMoveForward(piece, currentRow, targetRow))
                {
                    validCells.Add(_boardManager.GetCell(targetRow, targetCol));
                }
                else if (_moveValidator.CanCapture(currentRow, currentCol, targetRow, targetCol, piece.PieceType))
                {
                    validCells.Add(_boardManager.GetCell(targetRow, targetCol));
                }
            }
        }
        return validCells;
    }
}