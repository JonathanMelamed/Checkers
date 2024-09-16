using System.Collections.Generic;
using UnityEngine;
public class CellHighlighter : MonoBehaviour
{
    public void HighlightCells(List<Cell> cells)
    {
        foreach (Cell cell in cells)
        {
            SetHighlight(cell, true);
        }
    }

    public void UnHighlightAllCells()
    {
        foreach (Cell cell in Object.FindObjectsOfType<Cell>())
        {
            SetHighlight(cell, false);
        }
    }

    private void SetHighlight(Cell cell, bool highlight)
    {
        Renderer cellRenderer = cell.GetComponent<Renderer>();
        if (cellRenderer != null)
        {
            cellRenderer.enabled = highlight;
        }
    }
}