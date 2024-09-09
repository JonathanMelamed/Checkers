using System;
using System.Threading.Tasks;
using UnityEngine;

public class TurnHandler
{
    private PieceType _currentColor;
    private PlayerInput _playerInput;
    private MoveValidator _moveValidator;
    private BoardManager _boardManager;

    public event Action<PieceType> OnTurnCompleted;

    public TurnHandler(PieceType startingColor, PlayerInput playerInput, MoveValidator moveValidator, BoardManager boardManager)
    {
        _currentColor = startingColor;
        _playerInput = playerInput;
        _moveValidator = moveValidator;
        _boardManager = boardManager;
    }

    public async Task DoTurn()
    {
        var (selectedPiece, targetPosition) = await _playerInput.GetPlayerMove(_currentColor);

        // if (_moveValidator.ValidateMove(selectedPiece, targetPosition, _currentType))
        // {
            _boardManager.MovePiece(selectedPiece, targetPosition);
            OnTurnCompleted?.Invoke(_currentColor);
            SwitchTurn();
        // }
    }

    private void SwitchTurn()
    {
        _currentColor = _currentColor == PieceType.Black
            ? PieceType.White
            : PieceType.Black;
    }
}