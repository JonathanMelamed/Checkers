using System.Threading.Tasks;
using UnityEngine;

public class PlayerInput
{
    private PieceSelector pieceSelector;
    private CellSelector cellSelector;

    public PlayerInput(InputReader inputReader)
    {
        pieceSelector = new PieceSelector(inputReader);
        cellSelector = new CellSelector(inputReader);
    }

    public async Task<(Piece selectedPiece, Cell targetPosition)> GetPlayerMove(PieceType currentPlayerType)
    {
        var selectedPiece = await pieceSelector.SelectPiece(currentPlayerType);
        var targetPosition = await cellSelector.SelectCell();
        return (selectedPiece, targetPosition);
    }
}