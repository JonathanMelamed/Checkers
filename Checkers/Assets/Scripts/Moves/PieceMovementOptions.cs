using System.Collections.Generic;
using UnityEngine;

public class PieceMovementOptions
{
    private readonly MoveFinder _moveFinder;
    private readonly CellHighlighter _cellHighlighter;

    private List<Cell> _validMoves = new List<Cell>();

    public PieceMovementOptions(CellHighlighter cellHighlighter, MoveFinder moveFinder)
    {
        _moveFinder = moveFinder;
        _cellHighlighter = cellHighlighter;
    }

    public List<Cell> GetValidMoves(Cell sourceCell, PieceType pieceType)
    {
        ClearMoves();

        List<Cell> captureMoves = _moveFinder.FindCaptureMoves(sourceCell, pieceType);

        if (captureMoves.Count > 0)
        {
            _validMoves.AddRange(captureMoves);
        }
        else
        {
            _validMoves.AddRange(_moveFinder.FindValidMoves(sourceCell, pieceType));
        }

        return new List<Cell>(_validMoves);
    }

    public bool HasCaptureMoves(Cell sourceCell, PieceType pieceType)
    {
        ClearMoves();
        return _moveFinder.FindCaptureMoves(sourceCell, pieceType).Count > 0;
    }

    public void HighlightValidMoves()
    {
        _cellHighlighter.HighlightCells(_validMoves);
    }

    public void UnHighlightValidMoves()
    {
        _cellHighlighter.UnHighlightAllCells();
    }

    private void ClearMoves()
    {
        if (_validMoves != null)
        {
            _validMoves.Clear();
        }
    }
}