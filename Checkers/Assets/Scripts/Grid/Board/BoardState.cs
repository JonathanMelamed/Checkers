using UnityEngine;
using Zenject;

public class BoardState
{
    public int BoardSize { get; }
    public (Cell cell, PieceType pieceType)[,] _board;

    [Inject]
    public BoardState(int boardSize)
    {
        BoardSize = boardSize;
        _board = new (Cell, PieceType)[boardSize, boardSize];
    }

    public PieceType GetPieceTypeInCell(int row, int column)
    {
        return IsWithinBounds(row, column) ? _board[row, column].pieceType : PieceType.Null;
    }

    public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
    {
        if (!IsWithinBounds(fromRow, fromCol) || !IsWithinBounds(toRow, toCol)) return;

        var pieceType = _board[fromRow, fromCol].pieceType;
        _board[toRow, toCol] = (_board[toRow, toCol].cell, pieceType);
        _board[fromRow, fromCol] = (_board[fromRow, fromCol].cell, PieceType.Null);
    }

    public Cell GetCell(int row, int column)
    {
        return IsWithinBounds(row, column) ? _board[row, column].cell : null;
    }

    public Cell GetCapturedPieceCell(int toRow, int toCol, int fromRow, int fromCol)
    {
        if (Mathf.Abs(fromRow - toRow) <= 1 || Mathf.Abs(fromCol - toCol) <= 1) return null;

        int rowStep = fromRow > toRow ? 1 : -1;
        int colStep = fromCol > toCol ? 1 : -1;
        int captureRow = toRow + rowStep;
        int captureCol = toCol + colStep;

        return IsWithinBounds(captureRow, captureCol) && GetPieceTypeInCell(captureRow, captureCol) != PieceType.Null
            ? GetCell(captureRow, captureCol)
            : null;
    }

    public void ClearCapturedPiece(Cell capturedCell)
    {
        _board[capturedCell.GetRow(), capturedCell.GetColumn()] = (_board[capturedCell.GetRow(), capturedCell.GetColumn()].cell, PieceType.Null);
    }

    public bool IsWithinBounds(int row, int column) => row >= 0 && row < BoardSize && column >= 0 && column < BoardSize;
}