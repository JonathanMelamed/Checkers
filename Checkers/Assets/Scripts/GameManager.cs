using Zenject;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputReader _inputReader;
    private BoardManager _boardManager;
    private PieceManager _pieceManager;
    private CellHighlighter _cellHighlighter;
    private MoveValidator _moveValidator;
    private MoveManager _moveManager;
    private PlayerInput _playerInput;
    private TurnHandler _turnHandler;
    private MoveFinder _moveFinder;

    [Inject]
    public void Construct(InputReader inputReader, BoardManager boardManager, PieceManager pieceManager, 
        CellHighlighter cellHighlighter, MoveValidator moveValidator, MoveManager moveManager,
        PlayerInput playerInput, TurnHandler turnHandler, MoveFinder moveFinder)
    {
        _inputReader = inputReader;
        _boardManager = boardManager;
        _pieceManager = pieceManager;
        _cellHighlighter = cellHighlighter;
        _moveValidator = moveValidator;
        _moveManager = moveManager;
        _playerInput = playerInput;
        _turnHandler = turnHandler;
        _moveFinder = moveFinder;

        if (_boardManager == null)
        {
            Debug.LogError("BoardManager is null in GameManager!");
        }
    }

    private void Start()
    {
        InitializeGame();
        _turnHandler.StartTurn();
    }

    private void InitializeGame()
    {
        SetupGameComponents();
        SubscribeToEvents();
    }

    private void SetupGameComponents()
    {
        _boardManager.Initialize();
        _moveManager.Initialize();
    }

    private void SubscribeToEvents()
    {
        _turnHandler.OnTurnCompleted += HandleTurnCompleted;
    }

    private void HandleTurnCompleted()
    {
        _turnHandler.StartTurn();
    }
}