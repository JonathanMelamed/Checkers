using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private Dictionary<Vector2Int, Piece> piecePositions = new Dictionary<Vector2Int, Piece>();
    private BoardManager _boardManager;

    // Assuming BoardManager is assigned via the inspector or another method
    public void Initialize(BoardManager boardManager)
    {
        _boardManager = boardManager;
    }

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

    // New method to find the position of a given piece
    public Vector2Int? FindPiecePosition(Piece piece)
    {
        for (int row = 0; row < _boardManager.boardSize; row++)
        {
            for (int col = 0; col < _boardManager.boardSize; col++)
            {
                // Get the piece type in the current cell from BoardManager
                if (_boardManager.GetPieceTypeInCell(row, col) == piece.PieceType)
                {
                    // Assuming pieces have unique PieceTypes, return the position as Vector2Int
                    return new Vector2Int(row, col);
                }
            }
        }

        // Return null if the piece is not found on the board
        return null;
    }
}