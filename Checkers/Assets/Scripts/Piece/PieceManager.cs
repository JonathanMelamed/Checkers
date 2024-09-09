using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private List<Piece> piecePositions = new List<Piece>();
    private BoardManager _boardManager;

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
        for (int row = 0; row < _boardManager.boardSize; row++)
        {
            for (int col = 0; col < _boardManager.boardSize; col++)
            {
                if (_boardManager.GetPieceTypeInCell(row, col) == piece.PieceType)
                {
                    return new Vector2Int(row, col);
                }
            }
        }
        return null;
    }
}