using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PlayerInput
{
    public PieceSelector PieceSelector { get; private set; }
    public CellSelector CellSelector { get; private set; }

    [Inject]
    public PlayerInput(PieceSelector pieceSelector, CellSelector cellSelector)
    {
        PieceSelector = pieceSelector;
        CellSelector = cellSelector;
    }
}