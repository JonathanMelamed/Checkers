using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
public class PieceSelector
{
    private Camera mainCamera;
    private InputReader inputReader;
    private const string PieceTag = "Piece"; 
    public event Action<Piece> OnPieceSelected;

    public PieceSelector(InputReader inputReader)
    {
        mainCamera = Camera.main;
        this.inputReader = inputReader;

        if (inputReader != null)
        {
            inputReader.GameplayInput.GamePlay.Select.performed += OnSelectPerformed;
            inputReader.GameplayInput.Enable();
        }
        else
        {
            Debug.LogError("InputReader is not assigned.");
        }
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        Piece piece = TryGetClickedPiece();
        if (piece != null)
        {
            OnPieceSelected?.Invoke(piece); // Trigger the event when a piece is selected
        }
    }

    private Piece TryGetClickedPiece()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(PieceTag))
            {
                return hit.collider.GetComponent<Piece>();
            }
        }
        return null;
    }

    public void Cleanup()
    {
        if (inputReader != null)
        {
            inputReader.GameplayInput.GamePlay.Select.performed -= OnSelectPerformed;
            inputReader.GameplayInput.Disable();
        }
    }
}