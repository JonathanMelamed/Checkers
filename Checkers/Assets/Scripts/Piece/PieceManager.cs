using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField]private BoardManager _boardManager;

    public void Initialize(BoardManager boardManager)
    {
        _boardManager = boardManager;
    }
    public void RemovePiece(Cell cell)
    {
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
    
    public void MovePiece(Piece piece,Vector2Int? piecePosition, Cell targetCell)
    {
            int fromRow = piecePosition.Value.x;
            int fromCol = piecePosition.Value.y;
            int toRow = targetCell.GetRow();
            int toCol = targetCell.GetColumn();
            piece.transform.position = targetCell.transform.position;
    }
}