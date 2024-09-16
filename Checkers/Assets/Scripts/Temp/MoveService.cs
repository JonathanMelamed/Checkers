// using System.Collections.Generic;
//
// public class MoveService : IMoveService
// {
//     private readonly MovementValidator _movementValidator;
//     private readonly CaptureValidator _captureValidator;
//
//     public MoveService(MovementValidator movementValidator, CaptureValidator captureValidator)
//     {
//         _movementValidator = movementValidator;
//         _captureValidator = captureValidator;
//     }
//
//
//     public bool IsMoveValid(Cell sourceCell, Cell targetCell, PieceType pieceType)
//     {
//         return _movementValidator.IsMoveValid(sourceCell, targetCell, pieceType);
//     }
//
//     public Cell CanCapture(Cell sourceCell, PieceType pieceType, (int rowOffset, int colOffset) direction)
//     {
//         return _captureValidator.CanCapture(sourceCell, pieceType, direction);
//     }
//
//     public IEnumerable<(int rowOffset, int colOffset)> GetMovementDirections(PieceType pieceType)
//     {
//         return DirectionProvider.GetMovementDirections(pieceType);
//     }
// }