using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PieceSelector
{
    private Camera mainCamera;
    private InputReader inputReader;

    public PieceSelector(InputReader inputReader)
    {
        mainCamera = Camera.main;
        this.inputReader = inputReader;
        inputReader.GameplayInput.GamePlay.Select.performed += OnSelectPerformed;
    }

    public async Task<Piece> SelectPiece(PieceType currentPlayerType)
    {
        Piece selectedPiece = null;

        while (selectedPiece == null)
        {
            if (inputReader.GameplayInput.GamePlay.Select.triggered)
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

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        // Handle the select performed event
    }
}