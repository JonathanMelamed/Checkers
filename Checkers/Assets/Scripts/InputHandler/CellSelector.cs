using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CellSelector
{
    private Camera _mainCamera;
    private InputReader _inputReader;
    private const string CellTag = "Cell"; // Tag for identifying cell objects

    public event Action<Cell> OnCellSelected; // Event triggered when a valid cell is selected

    [Inject]
    public CellSelector(InputReader inputReader, Camera mainCamera)
    {
        _inputReader = inputReader;
        _mainCamera = mainCamera;
        SubscribeToInputEvents();
    }

    private void SubscribeToInputEvents()
    {
        _inputReader.GameplayInput.GamePlay.Select.performed += OnSelectPerformed;
        _inputReader.GameplayInput.Enable();
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        Cell selectedCell = GetClickedCell();
        if (selectedCell != null)
        {
            OnCellSelected?.Invoke(selectedCell);
        }
    }

    private Cell GetClickedCell()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
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