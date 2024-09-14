using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TurnHandler
{
    private PieceType _currentColor;
    private PlayerInput _playerInput;
    private PieceMovementOptions _pieceMovementOptions;
    private PieceManager _pieceManager;
    private BoardManager _boardManager;
    private Piece _selectedPiece;
    private List<Cell> _validMoves;
    private List<Piece> _canSelectPieces;
    public event Action<PieceType> OnTurnCompleted; 
    public event Action<Piece> OnInvalidPieceSelected;
    public TurnHandler(PieceType startingColor, PlayerInput playerInput, PieceMovementOptions pieceMovementOptions,
        PieceManager pieceManager, BoardManager boardManager)
    {
        _currentColor = startingColor;
        _playerInput = playerInput;
        _pieceMovementOptions = pieceMovementOptions;
        _pieceManager = pieceManager;
        _boardManager = boardManager;

        _playerInput.PieceSelector.OnPieceSelected += HandlePieceSelected;
        _playerInput.CellSelector.OnCellSelected += HandleCellSelected;
    }

    public async Task StartTurn()
    {
        _canSelectPieces = await GetCanSelectPiecesAsync();
        Debug.Log("Selectable pieces: " + _canSelectPieces.Count);
    }

    private async Task<List<Piece>> GetCanSelectPiecesAsync()
    {
        var capturablePieces = new List<Piece>();
        var allPieces = _pieceManager.GetPiecesByType(_currentColor);

        foreach (var piece in allPieces)
        {
            Vector2Int? piecePosition = _pieceManager.FindPiecePosition(piece);
            if (piecePosition.HasValue)
            {
                Cell currentCell = _boardManager.GetCell(piecePosition.Value.x, piecePosition.Value.y);
                if (_pieceMovementOptions.HasCaptureMoves(currentCell, _currentColor))
                {
                    capturablePieces.Add(piece);
                }
            }

            await Task.Yield(); 
        }

        if (capturablePieces.Count > 0)
        {
            return capturablePieces;
        }
        return allPieces;
    }

    private void HandlePieceSelected(Piece piece)
    {
        if (_canSelectPieces.Contains(piece))
        {
            _selectedPiece = piece;
            Vector2Int? piecePosition = _pieceManager.FindPiecePosition(_selectedPiece);
            if (piecePosition.HasValue)
            {
                Cell currentCell = _boardManager.GetCell(piecePosition.Value.x, piecePosition.Value.y);
                _validMoves = _pieceMovementOptions.GetValidMoves(currentCell, _selectedPiece.PieceType);
                _pieceMovementOptions.HighlightValidMoves();
            }
        }
        else
        {
            OnInvalidPieceSelected?.Invoke(piece);
        }
    }

    private void HandleCellSelected(Cell selectedCell)
    {
        if (_validMoves != null && _validMoves.Contains(selectedCell))
        {
            Vector2Int? piecePosition = _pieceManager.FindPiecePosition(_selectedPiece);
            if (piecePosition.HasValue)
            {
                _boardManager.MovePiece(piecePosition.Value.x, piecePosition.Value.y, selectedCell.GetRow(),
                    selectedCell.GetColumn());
                _pieceMovementOptions.HighlightValidMoves();
                SwitchTurn();
                OnTurnCompleted?.Invoke(_currentColor);
            }
        }
    }

    private void SwitchTurn()
    {
        _currentColor = _currentColor == PieceType.Black ? PieceType.White : PieceType.Black;
    }
}