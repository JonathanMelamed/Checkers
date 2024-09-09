using System;
using System.Threading.Tasks;
using UnityEngine;

public class TurnHandler
{
    private PieceType _currentType;
    private PlayerInput _playerInput;
    private MoveValidator _moveValidator;
    private BoardManager _boardManager;

    public event Action<PieceType> OnTurnCompleted;

    public TurnHandler(PieceType startingType, PlayerInput playerInput, MoveValidator moveValidator, BoardManager boardManager)
    {
        _currentType = startingType;
        _playerInput = playerInput;
        _moveValidator = moveValidator;
        _boardManager = boardManager;
    }

    public async Task DoTurn()
    {
        var (selectedPiece, targetPosition) = await _playerInput.GetPlayerMove(_currentType);

        // if (_moveValidator.ValidateMove(selectedPiece, targetPosition, _currentType))
        // {
            _boardManager.MovePiece(selectedPiece, targetPosition);
            OnTurnCompleted?.Invoke(_currentType);
            SwitchTurn();
        // }
    }

    private void SwitchTurn()
    {
        _currentType = _currentType == PieceType.Black
            ? PieceType.White
            : PieceType.Black;
    }
}