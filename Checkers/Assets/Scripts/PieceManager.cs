using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private Dictionary<Vector2Int, Piece> piecePositions = new Dictionary<Vector2Int, Piece>();

    public void SetPiece(Vector2Int position, Piece piece)
    {
        piecePositions[position] = piece;
    }

    public Piece GetPiece(Vector2Int position)
    {
        piecePositions.TryGetValue(position, out var piece);
        return piece;
    }

    public void RemovePiece(Vector2Int position)
    {
        piecePositions.Remove(position);
    }

    public bool IsPositionOccupied(Vector2Int position)
    {
        return piecePositions.ContainsKey(position);
    }
}