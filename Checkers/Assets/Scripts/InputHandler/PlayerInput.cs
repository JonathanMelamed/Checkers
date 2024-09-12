using System.Threading.Tasks;
using UnityEngine;

public class PlayerInput
{
    public PieceSelector PieceSelector { get; private set; }
    public CellSelector CellSelector { get; private set; }
    public PlayerInput(InputReader inputReader)
    {
        PieceSelector = new PieceSelector(inputReader);
        CellSelector = new CellSelector(inputReader);
    }
}