using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private PieceManager _pieceManager;
    [SerializeField] private PieceMovementOptions pieceMovementOptions;

    private PlayerInput playerInput;
    private TurnHandler turnHandler;

    private void Start()
    {
        playerInput = new PlayerInput(_inputReader);
        _pieceManager.Initialize(_boardManager);
        turnHandler = new TurnHandler(PieceType.White, playerInput, pieceMovementOptions, _pieceManager, _boardManager);
        StartGameLoop();
    }

    private async void StartGameLoop()
    {
        while (true)
        {
            await turnHandler.DoTurn();
        }
    }
}