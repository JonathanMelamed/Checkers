using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PieceManager : MonoBehaviour, IPieceManager
{
    [Inject] private BoardManager _boardManager;
    [Inject] public PieceFinder PieceFinder;
    [Inject] private PieceMover _pieceMover;

    public event Action PieceMoveDone;

    private void OnEnable()
    {
        if (_boardManager == null)
        {
            Debug.LogError("BoardManager is null in PieceManager!");
        }
        else
        {
            _boardManager.PieceCaptured += RemovePiece;
            _boardManager.PieceMoved += MovePiece;
        }
    }

    private void OnDisable()
    {
        _boardManager.PieceCaptured -= RemovePiece;
        _boardManager.PieceMoved -= MovePiece;
    }

    public void SubscribeToTurnHandler(TurnHandler turnHandler)
    {
        turnHandler.OnInvalidPieceSelected += HandleInvalidPieceSelected;
    }

    private async void HandleInvalidPieceSelected(Piece piece)
    {
        await PieceAnimator.ShakePieceAsync(piece);
    }

    public List<Piece> GetPiecesByType(PieceType pieceType)
    {
        return PieceFinder.GetPiecesByType(pieceType);
    }

    public void RemovePiece(Cell cell)
    {
        Piece piece = PieceFinder.FindPieceInCell(cell);
        if (piece != null)
        {
            Destroy(piece.gameObject);
        }
    }

    public void MovePiece(Cell fromCell, Cell toCell)
    {
        Piece piece = PieceFinder.FindPieceInCell(fromCell);

        if (piece != null)
        {
            Vector2Int? piecePosition = PieceFinder.FindPiecePosition(piece);
            if (piecePosition.HasValue)
            {
                _pieceMover.MovePiece(piece, piecePosition, toCell);
            }
        }

        PieceMoveDone?.Invoke();
    }
}