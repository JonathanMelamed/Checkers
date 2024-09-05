using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    public event Action<Vector2Int, Vector2Int> OnPieceMoved;

    private PieceManager _pieceManager;
    private MoveValidator _moveValidator;

    void Start()
    {
        _pieceManager = GetComponent<PieceManager>();
        _moveValidator = GetComponent<MoveValidator>();

        if (_pieceManager == null || _moveValidator == null)
        {
            Debug.LogError("Board requires PieceManager and MoveValidator components.");
        }
    }

    public void MovePiece(Vector2Int from, Vector2Int to)
    {
        if (_moveValidator.ValidateMove(from, to))
        {
            Piece piece = _pieceManager.GetPiece(from);
            _pieceManager.RemovePiece(from);
            _pieceManager.SetPiece(to, piece);

            OnPieceMoved?.Invoke(from, to);
        }
    }

    public void RemovePiece(Vector2Int position)
    {
        _pieceManager.RemovePiece(position);
    }
}