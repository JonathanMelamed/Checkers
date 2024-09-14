using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int boardSize = 8;
    private (Cell cell, PieceType pieceType)[,] _board;
    public event Action<Cell> PieceCaptured;
    public event Action<Cell,Cell> PieceMoved;

    void Awake()
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

    public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
    {
        if (IsWithinBounds(fromRow, fromCol) && IsWithinBounds(toRow, toCol))
        {
            PieceType pieceType = _board[fromRow, fromCol].pieceType;
            Cell capturedCell = GetCapturedPieceCell(toRow, toCol, fromRow, fromCol);
            if (capturedCell != null)
            {
                ClearCapturedPiece(capturedCell);
            }
            _board[toRow, toCol] = (_board[toRow, toCol].cell, pieceType);
            _board[fromRow, fromCol] = (_board[fromRow, fromCol].cell, PieceType.Null);
            PieceMoved?.Invoke(GetCell(fromRow, fromCol),GetCell(toRow, toCol));
        }
    }

    private Cell GetCapturedPieceCell(int toRow, int toCol, int fromRow, int fromCol)
    {
        if (Mathf.Abs(fromRow - toRow) <= 1 || Mathf.Abs(fromCol - toCol) <= 1)
        {
            return null;
        }

        int rowStep = (fromRow > toRow) ? 1 : -1;
        int colStep = (fromCol > toCol) ? 1 : -1;

        int currentRow = toRow + rowStep;
        int currentCol = toCol + colStep;

        if (IsWithinBounds(currentRow, currentCol))
        {
            if (GetPieceTypeInCell(currentRow, currentCol) != PieceType.Null)
            {
                return GetCell(currentRow, currentCol);
            }
        }
        return null;
    }


    private void ClearCapturedPiece(Cell capturedCell)
    {
        int row = capturedCell.GetRow();
        int col = capturedCell.GetColumn();
        _board[row, col] = (_board[row, col].cell, PieceType.Null);
        PieceCaptured?.Invoke(capturedCell);
    }

    public Cell GetCell(int row, int column)
    {
        if (IsWithinBounds(row, column))
        {
            return _board[row, column].cell;
        }

        return null;
    }

    public bool IsQueen(PieceType pieceType)
    {
        return false;
    }

    public bool IsWithinBounds(int row, int column)
    {
        return row >= 0 && column >= 0 && row < boardSize && column < boardSize;
    }
}