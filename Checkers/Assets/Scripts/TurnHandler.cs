using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
public class TurnHandler
{
    private PieceType _currentColor;
    private PlayerInput _playerInput;
    private PieceMovementOptions _pieceMovementOptions;
    private PieceManager _pieceManager;
    private BoardManager _boardManager;

    public event Action<PieceType> OnTurnCompleted;

    public TurnHandler(PieceType startingColor, PlayerInput playerInput, PieceMovementOptions pieceMovementOptions, PieceManager pieceManager, BoardManager boardManager)
    {
        _currentColor = startingColor;
        _playerInput = playerInput;
        _pieceMovementOptions = pieceMovementOptions;
        _pieceManager = pieceManager;
        _boardManager = boardManager;
    }

    public async Task DoTurn()
    {
        var (selectedPiece, targetPosition) = await _playerInput.GetPlayerMove(_currentColor);

        Vector2Int? piecePosition = _pieceManager.FindPiecePosition(selectedPiece);
        if (piecePosition.HasValue)
        {
            Cell currentCell = _boardManager.GetCell(piecePosition.Value.x, piecePosition.Value.y);

            List<Cell> validMoves = _pieceMovementOptions.GetValidMoves(currentCell, selectedPiece);
            _pieceMovementOptions.HighlightValidMoves(validMoves, Color.green);

            if (validMoves.Contains(targetPosition))
            {
                _boardManager.MovePiece(selectedPiece, targetPosition);
                OnTurnCompleted?.Invoke(_currentColor);
                SwitchTurn();
            }
        }
    }

    private void SwitchTurn()
    {
        _currentColor = _currentColor == PieceType.Black
            ? PieceType.White
            : PieceType.Black;
    }
}