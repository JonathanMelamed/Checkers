using System.Collections.Generic;
using UnityEngine;

public class PieceMovementOptions : MonoBehaviour
{
    [SerializeField] private MoveValidator _moveValidator;
    [SerializeField] private BoardManager _boardManager;

    private List<Cell> validMoves = new List<Cell>();
    private List<Cell> captureMoves = new List<Cell>();

    public List<Cell> GetValidMoves(Cell sourceCell, PieceType pieceType)
    {
        ClearMoves();
        FindCaptureMoves(sourceCell, pieceType);

        if (HasCaptureMoves(sourceCell, pieceType))
        {
            validMoves = new List<Cell>(captureMoves); // Only capture moves are valid if available
        }
        else
        {
            FindNonCaptureMoves(sourceCell, pieceType);
        }

        return validMoves;
    }

    public bool HasCaptureMoves(Cell sourceCell, PieceType pieceType)
    {
        ClearMoves();
        FindCaptureMoves(sourceCell, pieceType);
        return captureMoves.Count > 0;
    }

    private void ClearMoves()
    {
        validMoves.Clear();
        captureMoves.Clear();
    }

    private void FindCaptureMoves(Cell sourceCell, PieceType pieceType)
    {
        foreach (var direction in _moveValidator.GetMovementDirections(pieceType))
        {
            Cell captureCell = _moveValidator.CanCapture(sourceCell, pieceType, direction);
            if (captureCell != null)
            {
                captureMoves.Add(captureCell);
            }
        }
    }

    private void FindNonCaptureMoves(Cell sourceCell, PieceType pieceType)
    {
        int currentRow = sourceCell.GetRow();
        int currentCol = sourceCell.GetColumn();

        foreach (var direction in _moveValidator.GetMovementDirections(pieceType))
        {
            int targetRow = currentRow + direction.rowOffset;
            int targetCol = currentCol + direction.colOffset;
            Cell targetCell = _boardManager.GetCell(targetRow, targetCol);

            if (targetCell != null && _moveValidator.IsMoveValid(sourceCell, targetCell, pieceType))
            {
                validMoves.Add(targetCell);
            }
        }
    }

    public void HighlightValidMoves()
    {
        UnHighlightValidMoves();
        foreach (Cell cell in validMoves)
        {
            Renderer cellRenderer = cell.GetComponent<Renderer>();
            cellRenderer.enabled = true;
        }
    }

    private void UnHighlightValidMoves()
    {
        foreach (Cell cell in FindObjectsOfType<Cell>())
        {
            Renderer cellRenderer = cell.GetComponent<Renderer>();
            cellRenderer.enabled = false;
        }
    }
}
