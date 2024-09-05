using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CellManager : MonoBehaviour
{
    private Dictionary<Vector2Int, GameObject> cellMap = new Dictionary<Vector2Int, GameObject>();
    public int cubesPerRow; 

    public void InitializeCells()
    {
    }

    public GameObject GetCell(Vector2Int position)
    {
        return new GameObject();
    }
}