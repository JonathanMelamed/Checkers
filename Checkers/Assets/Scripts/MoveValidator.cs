using UnityEngine;
using System;
using System.Collections.Generic;

public class MoveValidator : MonoBehaviour
{
    public event Action<Vector2Int, Vector2Int> OnValidMove;

    private GridManager gridManager;
    private PieceManager pieceManager;

    void Start()
    {
        gridManager = GetComponent<GridManager>();
        pieceManager = GetComponent<PieceManager>();

        if (gridManager == null || pieceManager == null)
        {
            Debug.LogError("MoveValidator requires GridManager and PieceManager components.");
        }
    }

    public bool ValidateMove(Vector2Int from, Vector2Int to)
    {
        bool isValid = gridManager.IsValidPosition(to) && !pieceManager.IsPositionOccupied(to);
        if (isValid)
        {
            OnValidMove?.Invoke(from, to);
        }
        return isValid;
    }

    public List<Vector2Int> GetValidMoves(Vector2Int position)
    {
        // Implement logic to get valid moves for a piece at a given position
        return new List<Vector2Int>();
    }
}