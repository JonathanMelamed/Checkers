using System.Collections.Generic;
using UnityEngine;

public class PieceFinder
{
    private readonly BoardManager _boardManager;

    public PieceFinder(BoardManager boardManager)
    {
        _boardManager = boardManager;
    }

    public List<Piece> GetPiecesByType(PieceType pieceType)
    {
        List<Piece> piecesOfType = new List<Piece>();
        foreach (Piece piece in Object.FindObjectsOfType<Piece>())
        {
            if (piece.PieceType == pieceType)
            {
                piecesOfType.Add(piece);
            }
        }
        return piecesOfType;
    }

    public Piece FindPieceInCell(Cell cell)
    {
        Vector2Int cellPosition = new Vector2Int(cell.GetRow(), cell.GetColumn());

        foreach (Piece piece in Object.FindObjectsOfType<Piece>())
        {
            Vector2Int? piecePosition = FindPiecePosition(piece);
            if (piecePosition.HasValue && piecePosition.Value == cellPosition)
            {
                return piece;
            }
        }

        return null;
    }

    public Vector2Int? FindPiecePosition(Piece piece)
    {
        float minDistance = Mathf.Infinity;
        Vector2Int? closestCellPosition = null;

        for (int row = 0; row < _boardManager.BoardSize; row++)
        {
            for (int col = 0; col < _boardManager.BoardSize; col++)
            {
                Cell cell = _boardManager.GetCell(row, col);
                if (cell != null)
                {
                    float distance = Vector3.Distance(piece.transform.position, cell.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestCellPosition = new Vector2Int(row, col);
                    }
                }
            }
        }

        return closestCellPosition;
    }
}