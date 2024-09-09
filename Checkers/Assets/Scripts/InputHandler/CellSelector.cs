using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class CellSelector
{
    private Camera mainCamera;
    private InputReader _inputReader;

    public CellSelector(InputReader inputReader)
    {
        mainCamera = Camera.main;
        _inputReader = inputReader;

        if (_inputReader != null)
        {
            _inputReader.GameplayInput.GamePlay.Select.performed += OnSelectPerformed;
            _inputReader.GameplayInput.Enable(); // Ensure the input actions are enabled
        }
        else
        {
            Debug.LogError("InputReader is not assigned.");
        }
    }

    public async Task<Cell> SelectCell()
    {
        Cell selectedCell = null;

        while (selectedCell == null)
        {
            if (_inputReader.GameplayInput.GamePlay.Select.triggered)
            {
                selectedCell = TryGetClickedCell();
            }
            await Task.Yield();
        }

        return selectedCell;
    }

    private Cell TryGetClickedCell()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            return cell;
        }
        return null;
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
    }

    public void Cleanup()
    {
        if (_inputReader != null)
        {
            _inputReader.GameplayInput.GamePlay.Select.performed -= OnSelectPerformed;
            _inputReader.GameplayInput.Disable();
        }
    }
}