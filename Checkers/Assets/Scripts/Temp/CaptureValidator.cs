// using UnityEngine;
//
// public class CaptureValidator
// {
//     private readonly BoardManager _boardManager;
//
//     public CaptureValidator(BoardManager boardManager)
//     {
//         _boardManager = boardManager;
//     }
//
//     public Cell CanCapture(Cell sourceCell, PieceType pieceType, (int rowOffset, int colOffset) direction)
//     {
//         Cell opponentCell = GetOpponentCell(sourceCell, direction);
//         if (opponentCell != null && IsOpponentPiece(opponentCell, pieceType))
//         {
//             return GetCaptureCell(sourceCell, direction);
//         }
//
//         return null;
//     }
//
//     private Cell GetOpponentCell(Cell sourceCell, (int rowOffset, int colOffset) direction)
//     {
//         int fromRow = sourceCell.GetRow();
//         int fromCol = sourceCell.GetColumn();
//         return _boardManager.GetCell(fromRow + direction.rowOffset, fromCol + direction.colOffset);
//     }
//
//     private Cell GetCaptureCell(Cell sourceCell, (int rowOffset, int colOffset) direction)
//     {
//         int fromRow = sourceCell.GetRow();
//         int fromCol = sourceCell.GetColumn();
//         return _boardManager.GetCell(fromRow + 2 * direction.rowOffset, fromCol + 2 * direction.colOffset);
//     }
//
//     private bool IsOpponentPiece(Cell opponentCell, PieceType pieceType)
//     {
//         PieceType opponentPieceType = _boardManager.GetPieceTypeInCell(opponentCell.GetRow(), opponentCell.GetColumn());
//         return opponentPieceType != PieceType.Null && opponentPieceType != pieceType;
//     }
// }
