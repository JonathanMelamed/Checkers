using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private int _row;
    [SerializeField] private int _column;

    public int GetRow()
    {
        return _row;
    }

    public int GetColumn()
    {
        return _column;
    }
}