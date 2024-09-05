using UnityEngine;
using System;

public class Piece : MonoBehaviour
{
    public bool IsQueen { get; private set; }

    public event Action<Piece> OnPieceCaptured;

    // Method to move the piece to a new position
    public void MovePiece(Vector2Int newPosition)
    {
        // Implement logic to move the piece
        // For example, update the position in the Cell or BoardManager
    }

    // Method to promote the piece to a queen
    public void Promote()
    {
        IsQueen = true;
        // Implement additional logic for promotion if necessary
        // For example, update the piece's appearance or abilities
    }

    // Method to call when the piece is captured
    public void Capture()
    {
        OnPieceCaptured?.Invoke(this);
        // Implement additional logic for capturing the piece
        // For example, notify other managers or update the board state
    }
}