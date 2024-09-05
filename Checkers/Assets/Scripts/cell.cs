using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private int _row;
    [SerializeField] private int _column;

    public bool IsOccupied()
    {
        return false;
    }

    public void SetPiece(Piece newPiece)
    {
    }

    public void ClearPiece()
    {
    }
}