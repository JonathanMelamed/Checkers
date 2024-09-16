using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveManager : MonoBehaviour
{
    [Inject] private PieceMovementOptions _pieceMovementOptions;
    [Inject] private BoardManager _boardManager;
    [Inject] private PieceManager _pieceManager;
    [Inject] private CellHighlighter _cellHighlighter;
    [Inject] private MoveFinder _moveFinder;

    private List<Cell> _validMoves;

    public void Initialize()
    {
        _pieceMovementOptions = new PieceMovementOptions(_cellHighlighter, _moveFinder);
    }

    public List<Piece> GetSelectablePieces(PieceType currentColor)
    {
        var capturablePieces = new List<Piece>();
        foreach (var piece in _pieceManager.GetPiecesByType(currentColor))
        {
            Vector2Int? piecePosition = _pieceManager.PieceFinder.FindPiecePosition(piece);
            if (piecePosition == null)
            {
                Debug.LogError("The Game Is Broken From It Root!");
                continue;
            }

            Cell fromCell = _boardManager.GetCell(piecePosition.Value.x, piecePosition.Value.y);
            if (_pieceMovementOptions.HasCaptureMoves(fromCell, currentColor))
            {
                capturablePieces.Add(piece);
            }
        }

        return capturablePieces.Count > 0 ? capturablePieces : _pieceManager.GetPiecesByType(currentColor);
    }

    public void HandlePieceSelected(Piece piece, PieceType currentColor)
    {
        _validMoves = new List<Cell>();
        _pieceMovementOptions.UnHighlightValidMoves();
        Vector2Int? piecePosition = _pieceManager.PieceFinder.FindPiecePosition(piece);
        if (piecePosition.HasValue)
        {
            var currentCell = _boardManager.GetCell(piecePosition.Value.x, piecePosition.Value.y);
            _validMoves = _pieceMovementOptions.GetValidMoves(currentCell, piece.PieceType);
            _pieceMovementOptions.HighlightValidMoves();
        }
    }

    public void ClearValidMoves()
    {
        _pieceMovementOptions.UnHighlightValidMoves();
        _validMoves = new List<Cell>();
    }

    public bool HandleCellSelected(Cell selectedCell, Piece selectedPiece)
    {
        if (_validMoves != null && _validMoves.Contains(selectedCell))
        {
            var piecePosition = _pieceManager.PieceFinder.FindPiecePosition(selectedPiece);
            if (piecePosition.HasValue)
            {
                _boardManager.MovePiece(piecePosition.Value.x, piecePosition.Value.y, selectedCell.GetRow(),
                    selectedCell.GetColumn());
                return true;
            }
        }
        else
        {
            Debug.LogWarning("Invalid move.");
        }

        return false;
    }
}
