using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class PieceSelector
{
    private Camera mainCamera;

    public PieceSelector()
    {
        mainCamera = Camera.main;
    }

    public async Task<Piece> SelectPiece(PieceType currentPlayerType)
    {
        Piece selectedPiece = null;

        while (selectedPiece == null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                var piece = TryGetClickedPiece();
                if (IsValidPiece(piece, currentPlayerType))
                {
                    selectedPiece = piece;
                }
            }
            await Task.Yield();
        }

        return selectedPiece;
    }

    private Piece TryGetClickedPiece()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Piece piece = hit.collider.GetComponent<Piece>();
            return piece;
        }
        return null;
    }

    private bool IsValidPiece(Piece piece, PieceType currentPlayerType)
    {
        return piece != null && piece.PieceType == currentPlayerType;
    }
}