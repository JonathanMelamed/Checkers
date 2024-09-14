using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private PieceManager _pieceManager;
    [SerializeField] private PieceMovementOptions _pieceMovementOptions;

    private PlayerInput _playerInput;
    private TurnHandler _turnHandler;

    private void Start()
    {
        InitializeGame();
        _turnHandler.StartTurn();
    }

    private void InitializeGame()
    {
        InitializePlayerInput();
        InitializeTurnHandler();
        SubscribeToEvents();
    }

    private void InitializePlayerInput()
    {
        _playerInput = new PlayerInput(_inputReader);
    }

    private void InitializeTurnHandler()
    {
        _turnHandler = new TurnHandler(PieceType.White, _playerInput, _pieceMovementOptions, _pieceManager,
            _boardManager);
        _pieceManager.SubscribeToTurnHandler(_turnHandler);
    }

    private void SubscribeToEvents()
    {
        _turnHandler.OnTurnCompleted += HandleTurnCompleted;
    }

    private void HandleTurnCompleted(PieceType completedColor)
    {
        _turnHandler.StartTurn();
    }
}