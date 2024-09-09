using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class CellSelector
{
    private Camera mainCamera;
    
    public CellSelector()
    {
        mainCamera = Camera.main;
    }

    public async Task<Cell> SelectCell()
    {
        Cell selectedCell = null;

        while (selectedCell == null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
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
            // Ensure that the clicked object is a Cell
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell != null)
            {
                return cell;
            }
        }
        return null;
    }
}