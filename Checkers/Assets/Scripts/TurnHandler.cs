using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class TurnHandler
{
    private PieceType _currentColor;
    private readonly InputHandler _inputHandler;
    private readonly MoveManager _moveManager;
    private readonly PieceManager _pieceManager;
    private Piece _selectedPiece;
    private bool _inputLocked;

    public PieceType CurrentColor => _currentColor;
    public event Action OnTurnCompleted;
    public event Action<Piece> OnInvalidPieceSelected;

    public List<Piece> CanSelectPieces { get; private set; }
    [Inject]
    public TurnHandler(PieceType startingColor, PlayerInput playerInput, BoardManager boardManager,
        MoveManager moveManager, PieceManager pieceManager)
    {
        _currentColor = startingColor;
        _inputHandler = new InputHandler(this, playerInput);
        _moveManager = moveManager;
        _pieceManager = pieceManager;
        _pieceManager.PieceMoveDone += HandleTurnCompleted;
    }
    public void StartTurn()
    {
        _inputLocked = true;
        CanSelectPieces = _moveManager.GetSelectablePieces(_currentColor);
        Debug.Log("Selectable pieces: " + CanSelectPieces.Count);
        _inputLocked = false;
    }

    public void HandlePieceSelected(Piece piece)
    {
        if (_inputLocked)
        {
            Debug.LogWarning("Input is locked, please wait for the turn to start.");
            return;
        }

        _selectedPiece = piece;
        if (CanSelectPieces.Contains(piece))
        {
            _moveManager.HandlePieceSelected(piece, _currentColor);
        }
        else
        {
            _moveManager.ClearValidMoves();
            RaiseInvalidPieceSelected(piece);
        }
    }

    public void HandleCellSelected(Cell selectedCell)
    {
        if (_inputLocked)
        {
            Debug.LogWarning("Input is locked, please wait for the turn to start.");
            return;
        }

        if (_selectedPiece != null)
        {
            _moveManager.HandleCellSelected(selectedCell, _selectedPiece);
        }
    }

    public void SwitchTurn()
    {
        _currentColor = _currentColor == PieceType.Black ? PieceType.White : PieceType.Black;
    }

    private void RaiseInvalidPieceSelected(Piece piece)
    {
        OnInvalidPieceSelected?.Invoke(piece);
    }

    private void HandleTurnCompleted()
    {
        SwitchTurn();
        OnTurnCompleted.Invoke();
    }
}