using System.Collections.Generic;
using UnityEngine;

public class MoveFinder
{
    private readonly MoveValidator _moveValidator;
    private readonly BoardManager _boardManager;

    public MoveFinder(MoveValidator moveValidator, BoardManager boardManager)
    {
        _moveValidator = moveValidator;
        _boardManager = boardManager;
    }

    public List<Cell> FindValidMoves(Cell sourceCell, PieceType pieceType)
    {
        var validMoves = new List<Cell>();
        foreach (var direction in MoveValidator.GetMovementDirections(pieceType))
        {
            Cell targetCell = GetTargetCell(sourceCell, direction.rowOffset, direction.colOffset);
            if (targetCell != null && _moveValidator.IsMoveValid(sourceCell, targetCell, pieceType))
                validMoves.Add(targetCell);
        }
        return validMoves;
    }

    public List<Cell> FindCaptureMoves(Cell sourceCell, PieceType pieceType)
    {
        var captureMoves = new List<Cell>();
        foreach (var direction in MoveValidator.GetMovementDirections(pieceType))
        {
            Cell captureCell = _moveValidator.CanCapture(sourceCell, pieceType, direction);
            if (captureCell != null) captureMoves.Add(captureCell);
        }
        return captureMoves;
    }

    private Cell GetTargetCell(Cell sourceCell, int rowOffset, int colOffset)
    {
        int targetRow = sourceCell.GetRow() + rowOffset, targetCol = sourceCell.GetColumn() + colOffset;
        return _boardManager.GetCell(targetRow, targetCol);
    }
}