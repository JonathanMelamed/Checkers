using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CellSelector
{
    private Camera mainCamera;
    private InputReader inputReader;
    private const string CellTag = "Cell"; // Define the tag for cells

    public event Action<Cell> OnCellSelected; // Event triggered when a valid cell is selected

    public CellSelector(InputReader inputReader)
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
        Cell selectedCell = TryGetClickedCell();
        if (selectedCell != null)
        {
            OnCellSelected?.Invoke(selectedCell);
        }
    }

    private Cell TryGetClickedCell()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(CellTag)) 
            {
                return hit.collider.GetComponent<Cell>();
            }
        }
        return null;
    }
}