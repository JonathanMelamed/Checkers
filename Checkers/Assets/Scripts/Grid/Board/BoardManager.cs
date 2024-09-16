using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private int _boardSize = 8;
    public int BoardSize => _boardSize;

    private BoardState _boardState;
    private BoardInitializer _boardInitializer;

    public event Action<Cell> PieceCaptured;
    public event Action<Cell, Cell> PieceMoved;

    [Inject]
    public void Construct(BoardState boardState, BoardInitializer boardInitializer)
    {
        _boardState = boardState;
        _boardInitializer = boardInitializer;
    }

    public void Initialize()
    {
        var cells = GetCells();
        _boardInitializer.InitializeBoard(cells);
    }

    private IEnumerable<Cell> GetCells() => FindObjectsOfType<Cell>();

    public PieceType GetPieceTypeInCell(int row, int column) => _boardState.GetPieceTypeInCell(row, column);

    public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
    {
        Cell capturedCell = _boardState.GetCapturedPieceCell(toRow, toCol, fromRow, fromCol);
        if (capturedCell != null)
        {
            _boardState.ClearCapturedPiece(capturedCell);
            PieceCaptured?.Invoke(capturedCell);
        }

        _boardState.MovePiece(fromRow, fromCol, toRow, toCol);
        PieceMoved?.Invoke(_boardState.GetCell(fromRow, fromCol), _boardState.GetCell(toRow, toCol));
    }

    public Cell GetCell(int row, int column) => _boardState.GetCell(row, column);

    public bool IsWithinBounds(int row, int column) => _boardState.IsWithinBounds(row, column);
}