using UnityEngine;

public class PieceMover
{
    public void MovePiece(Piece piece, Vector2Int? piecePosition, Cell targetCell)
    {
        if (piecePosition.HasValue)
        {
            piece.transform.position = targetCell.transform.position;
        }
    }
}