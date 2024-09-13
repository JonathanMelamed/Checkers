using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private BoardManager _boardManager;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _boardManager.PieceCaptured += OnPieceCaptured;
        _boardManager.PieceMoved += OnPieceMoved;
    }

    private void OnPieceCaptured(Cell capturedCell)
    {
        RemovePiece(capturedCell);
    }
    private void OnPieceMoved(Cell fromCell,Cell toCell)
    {
        Piece piece = FindPieceInCell(fromCell);

        if (piece != null)
        {
            Vector2Int? piecePosition = FindPiecePosition(piece);
            if (piecePosition.HasValue)
            {
                MovePiece(piece, piecePosition.Value, toCell);
            }
        }
    }

    public void RemovePiece(Cell cell)
    {
        Piece piece = FindPieceInCell(cell);
        if (piece != null)
        {
            Destroy(piece.gameObject);
        }
    }

    private Piece FindPieceInCell(Cell cell)
    {
        Vector2Int cellPosition = new Vector2Int(cell.GetRow(), cell.GetColumn());

        foreach (Piece piece in FindObjectsOfType<Piece>())
        {
            Vector2Int? piecePosition = FindPiecePosition(piece);
            if (piecePosition.HasValue && piecePosition.Value == cellPosition)
            {
                return piece;
            }
        }

        return null;
    }

    [CanBeNull]
    public Vector2Int? FindPiecePosition(Piece piece)
    {
        float minDistance = Mathf.Infinity;
        Vector2Int? closestCellPosition = null;

        for (int row = 0; row < _boardManager.boardSize; row++)
        {
            for (int col = 0; col < _boardManager.boardSize; col++)
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

    public void MovePiece(Piece piece, Vector2Int? piecePosition, Cell targetCell)
    {
        int fromRow = piecePosition.Value.x;
        int fromCol = piecePosition.Value.y;
        int toRow = targetCell.GetRow();
        int toCol = targetCell.GetColumn();
        piece.transform.position = targetCell.transform.position;
    }
}