using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovementOptions : MonoBehaviour
{
    private MoveValidator _moveValidator;
    [SerializeField]private BoardManager _boardManager;
    public PieceMovementOptions(BoardManager boardManager, MoveValidator moveValidator)
    {
         _boardManager = boardManager;
        _moveValidator = moveValidator;
    }
    public List<Cell> GetValidMoves(Cell sourceCell, Piece piece)
    {
        List<Cell> validCells = new List<Cell>();
        int currentRow = sourceCell.GetRow();
        int currentCol = sourceCell.GetColumn();

        int[] rowOffsets = { 1, -1 }; 
        int[] colOffsets = { 1, -1 }; 

        foreach (int rowOffset in rowOffsets)
        {
            foreach (int colOffset in colOffsets)
            {
                int targetRow = currentRow + rowOffset;
                int targetCol = currentCol + colOffset;

                if (_boardManager.IsWithinBounds(targetRow, targetCol) &&
                    _moveValidator.IsCellEmpty(targetRow, targetCol) &&
                    _moveValidator.IsMoveForward(piece, currentRow, targetRow))
                {
                    validCells.Add(_boardManager.GetCell(targetRow, targetCol));
                }
                else if (_moveValidator.CanCapture(currentRow, currentCol, targetRow, targetCol, piece.PieceType))
                {
                    validCells.Add(_boardManager.GetCell(targetRow, targetCol));
                }
            }
        }

        return validCells;
    }

    public void HighlightValidMoves(List<Cell> validCells, Color highlightColor)
    {
        foreach (Cell cell in validCells)
        {
            Renderer cellRenderer = cell.GetComponent<Renderer>();
            if (cellRenderer != null)
            {
                // Set material rendering mode to transparent
                Material cellMaterial = cellRenderer.material;
                cellMaterial.SetFloat("_Mode", 3); // 3 is for transparent mode in Unity's Standard shader
                cellMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                cellMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                cellMaterial.SetInt("_ZWrite", 0);
                cellMaterial.DisableKeyword("_ALPHATEST_ON");
                cellMaterial.EnableKeyword("_ALPHABLEND_ON");
                cellMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                cellMaterial.renderQueue = 3000;

                // Set the color and transparency (alpha channel)
                Color transparentColor = highlightColor;
                transparentColor.a = 0.5f; // Set the transparency (0 is fully transparent, 1 is fully opaque)
                cellMaterial.color = transparentColor;
            }
        }
    }
}
