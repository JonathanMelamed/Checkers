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
        HighlightValidMoves();
        validMoves.Clear();
        captureMoves.Clear();
        int currentRow = sourceCell.GetRow();
        int currentCol = sourceCell.GetColumn();
        foreach (var direction in _moveValidator.GetMovementDirections(pieceType))
        {
            Cell captureCell = _moveValidator.CanCapture(sourceCell, pieceType, direction);
            if (captureCell != null)
            {
                captureMoves.Add(captureCell);
            }
        }
        if (captureMoves.Count > 0)
        {
            validMoves = captureMoves;
        }
        else
        {
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

        HighlightValidMoves();
        return validMoves;
    }

    private void HighlightValidMoves()
    {
        foreach (Cell cell in validMoves)
        {
            Renderer cellRenderer = cell.GetComponent<Renderer>();
            cellRenderer.enabled = !cellRenderer.enabled;
        }
    }
}
