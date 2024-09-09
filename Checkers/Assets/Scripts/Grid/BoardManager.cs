using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int boardSize = 8;
    private PieceManager _pieceManager;
    private (Cell cell, PieceType pieceType)[,] _board;

    void Start()
    {
        _board = new (Cell, PieceType)[boardSize, boardSize];
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        Cell[] cells = FindObjectsOfType<Cell>();

        foreach (Cell cell in cells)
        {
            int row = cell.GetRow();
            int column = cell.GetColumn();
            _board[row, column] = (cell, PieceType.Null); // Initialize each cell with no piece
        }

        SetupPieces();
    }

    private void SetupPieces()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                // Check if the cell should have a piece based on the row and column
                if ((row + col) % 2 == 1)
                {
                    if (row < 3)
                    {
                        _board[row, col] = (_board[row, col].cell, PieceType.White);
                    }
                    else if (row >= boardSize - 3)
                    {
                        _board[row, col] = (_board[row, col].cell, PieceType.Black);
                    }
                }
            }
        }
    }

    public PieceType GetPieceTypeInCell(int row, int column)
    {
        if (IsWithinBounds(row, column))
        {
            return _board[row, column].pieceType;
        }
        return PieceType.Null;
    }

    public void MovePiece(Piece pieceToMove,Cell targetCell)
    {
        Vector2Int? currentPosition = _pieceManager.FindPiecePosition(pieceToMove);
        int fromRow = currentPosition.Value.x;
        int fromColumn = currentPosition.Value.y;
        int toRow = targetCell.GetRow();
        int toColumn = targetCell.GetColumn();
        
        if (IsWithinBounds(fromRow, fromColumn) && IsWithinBounds(toRow, toColumn))
        {
            PieceType pieceType = _board[fromRow, fromColumn].pieceType;
            _board[toRow, toColumn] = (_board[toRow, toColumn].cell, pieceType);
            _board[fromRow, fromColumn] = (_board[fromRow, fromColumn].cell, PieceType.Null);
        }
    }

    private bool IsWithinBounds(int row, int column)
    {
        return row >= 0 && column >= 0 && row < boardSize && column < boardSize;
    }
}
