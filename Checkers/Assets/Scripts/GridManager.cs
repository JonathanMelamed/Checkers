using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    [SerializeField] private CellManager cellManager;

    void Start()
    {
        cellManager = GetComponent<CellManager>();
        if (cellManager == null)
        {
            Debug.LogError("GridManager requires a CellManager component.");
            return;
        }

        InitializeGrid();
    }

    public void InitializeGrid()
    {
        cellManager.InitializeCells();
    }

    public GameObject GetCell(Vector2Int position)
    {
        return cellManager.GetCell(position);
    }

    public bool IsValidPosition(Vector2Int position)
    {
        return false;
    }
}