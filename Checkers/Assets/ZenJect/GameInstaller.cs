using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PieceType startingColor;
    [SerializeField] private int boardSize = 8;
    public override void InstallBindings()
    {
        Container.Bind<InputReader>().FromInstance(inputReader).AsSingle();
        Container.Bind<PieceType>().FromInstance(startingColor).AsSingle();

        Container.Bind<BoardState>().ToSelf().AsSingle().WithArguments(boardSize);
        
        Container.Bind<BoardManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PieceManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CellHighlighter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MoveValidator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MoveManager>().FromComponentInHierarchy().AsSingle();

        Container.Bind<MoveFinder>().AsSingle();
        Container.Bind<BoardInitializer>().AsSingle();
        Container.Bind<PieceAnimator>().AsSingle();
        Container.Bind<PieceFinder>().AsSingle();
        Container.Bind<PieceMover>().AsSingle();
        Container.Bind<PieceMovementOptions>().AsSingle();
        Container.Bind<PieceSelector>().AsSingle();
        Container.Bind<CellSelector>().AsSingle();
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<TurnHandler>().AsSingle();

        Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();

        // Declare signals if used (optional)
        // Container.DeclareSignal<PieceCapturedSignal>();
        // Container.DeclareSignal<PieceMovedSignal>();
    }
}