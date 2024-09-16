// using UnityEngine;
//
// public class MovementValidator
// {
//     private readonly BoardManager _boardManager;
//
//     public MovementValidator(BoardManager boardManager)
//     {
//         _boardManager = boardManager;
//     }
//
//     public bool IsMoveValid(Cell sourceCell, Cell targetCell, PieceType pieceType)
//     {
//         return IsWithinBounds(targetCell) &&
//                IsMoveDiagonal(sourceCell, targetCell) &&
//                IsMoveForward(pieceType, sourceCell, targetCell) &&
//                IsCellEmpty(targetCell);
//     }
//
//     private bool IsWithinBounds(Cell cell)
//     {
//         return _boardManager.IsWithinBounds(cell.GetRow(), cell.GetColumn());
//     }
//
//     private bool IsMoveDiagonal(Cell sourceCell, Cell targetCell)
//     {
//         int rowDifference = Mathf.Abs(targetCell.GetRow() - sourceCell.GetRow());
//         int colDifference = Mathf.Abs(targetCell.GetColumn() - sourceCell.GetColumn());
//         return rowDifference == colDifference;
//     }
//
//     private bool IsMoveForward(PieceType pieceType, Cell sourceCell, Cell targetCell)
//     {
//         int direction = (pieceType == PieceType.White) ? -1 : 1;
//         return (targetCell.GetRow() - sourceCell.GetRow()) * direction > 0;
//     }
//
//     private bool IsCellEmpty(Cell cell)
//     {
//         return _boardManager.GetPieceTypeInCell(cell.GetRow(), cell.GetColumn()) == PieceType.Null;
//     }
// }