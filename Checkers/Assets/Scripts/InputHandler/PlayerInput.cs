using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput
{
    private PieceSelector pieceSelector;
    private CellSelector cellSelector;

    public PlayerInput()
    {
        pieceSelector = new PieceSelector();
        cellSelector = new CellSelector();
    }

    public async Task<(Piece selectedPiece, Cell targetPosition)> GetPlayerMove(PieceType currentPlayerType)
    {
        var selectedPiece = await pieceSelector.SelectPiece(currentPlayerType);
        var targetPosition = await cellSelector.SelectCell();
        return (selectedPiece, targetPosition);
    }
}