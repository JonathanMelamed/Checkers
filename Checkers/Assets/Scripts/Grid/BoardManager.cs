using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int boardSize = 8;
    private (Cell cell, PieceType pieceType)[,] _board;

    void Start()
    {
        _board = new (Cell, PieceType)[boardSize, boardSize];
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        GameObject[] cellObjects = GameObject.FindGameObjectsWithTag("Cell");

        foreach (GameObject cellObject in cellObjects)
        {
            Cell cell = cellObject.GetComponent<Cell>();
            int row = cell.GetRow();
            int column = cell.GetColumn();
            _board[row, column] = (cell, PieceType.Null);
        }

        SetupPieces();
    }

    private void SetupPieces()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if ((row + col) % 2 == 0)
                {
                    if (row < 3) 
                    {
                        _board[row, col] = (_board[row, col].cell, PieceType.Black);
                    }
                    else if (row >= boardSize - 3)
                    {
                        _board[row, col] = (_board[row, col].cell, PieceType.White);
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

    public bool IsQueen(PieceType pieceType)
    {
        // Return whether the pieceType is a queen (can move in all directions).
        // You can add a specific logic to mark pieces as queens.
        return false;
    }

    public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
    {
        if (IsWithinBounds(fromRow, fromCol) && IsWithinBounds(toRow, toCol))
        {
            PieceType pieceType = _board[fromRow, fromCol].pieceType;
            _board[toRow, toCol] = (_board[toRow, toCol].cell, pieceType);
            _board[fromRow, fromCol] = (_board[fromRow, fromCol].cell, PieceType.Null);
        }
    }

    public Cell GetCell(int row, int column)
    {
        if (IsWithinBounds(row, column))
        {
            return _board[row, column].cell;
        }
        return null;
    }

    public bool IsWithinBounds(int row, int column)
    {
        return row >= 0 && column >= 0 && row < boardSize && column < boardSize;
    }
}