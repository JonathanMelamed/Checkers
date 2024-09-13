using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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

    public TurnHandler(PieceType startingColor, PlayerInput playerInput, PieceMovementOptions pieceMovementOptions, PieceManager pieceManager, BoardManager boardManager)
    {
        _currentColor = startingColor;
        _playerInput = playerInput;
        _pieceMovementOptions = pieceMovementOptions;
        _pieceManager = pieceManager;
        _boardManager = boardManager;

        _playerInput.PieceSelector.OnPieceSelected += HandlePieceSelected;
        _playerInput.CellSelector.OnCellSelected += HandleCellSelected; 
    }

    public async Task DoTurn()
    {
        _canSelectPieces = GetCanSelectPieces();
        await Task.Yield();
    }

    private List<Piece> GetCanSelectPieces()
    {
        //empty for now
        return null;
    }

    private void HandlePieceSelected(Piece piece)
    {
        if (piece.PieceType == _currentColor)
        {
            _selectedPiece = piece;
            Vector2Int? piecePosition = _pieceManager.FindPiecePosition(_selectedPiece);
            if (piecePosition.HasValue)
            {
                Cell currentCell = _boardManager.GetCell(piecePosition.Value.x, piecePosition.Value.y);
                _validMoves = _pieceMovementOptions.GetValidMoves(currentCell, _selectedPiece.PieceType);
            }
        }
    }

    private void HandleCellSelected(Cell selectedCell)
    {
        if (_validMoves != null && _validMoves.Contains(selectedCell))
        {
            Vector2Int? piecePosition = _pieceManager.FindPiecePosition(_selectedPiece);
            if (piecePosition.HasValue)
            {
                _boardManager.MovePiece(piecePosition.Value.x, piecePosition.Value.y,selectedCell.GetRow(),selectedCell.GetColumn());
                // _pieceManager.MovePiece(_selectedPiece, piecePosition, selectedCell);
                OnTurnCompleted?.Invoke(_currentColor);
                SwitchTurn();
            }
        }
    }

    private void SwitchTurn()
    {
        _currentColor = _currentColor == PieceType.Black ? PieceType.White : PieceType.Black;
    }
}