using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardInitializer
{
    private BoardState _boardState;

    [Inject]
    public BoardInitializer(BoardState boardState)
    {
        _boardState = boardState;
    }

    public void InitializeBoard(IEnumerable<Cell> cells)
    {
        InitializeCells(cells);
        SetupPieces();
    }

    private void InitializeCells(IEnumerable<Cell> cells)
    {
        foreach (var cell in cells)
        {
            int row = cell.GetRow();
            int column = cell.GetColumn();
            _boardState._board[row, column] = (cell, PieceType.Null);
        }
    }

    private void SetupPieces()
    {
        for (int row = 0; row < _boardState.BoardSize; row++)
        {
            for (int col = 0; col < _boardState.BoardSize; col++)
            {
                if ((row + col) % 2 == 0)
                {
                    if (row < 3)
                    {
                        _boardState._board[row, col] = (_boardState._board[row, col].cell, PieceType.Black);
                    }
                    else if (row >= _boardState.BoardSize - 3)
                    {
                        _boardState._board[row, col] = (_boardState._board[row, col].cell, PieceType.White);
                    }
                }
            }
        }
    }
}