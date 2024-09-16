using System.Collections.Generic;
using UnityEngine;

public interface IPieceManager
{
    void SubscribeToTurnHandler(TurnHandler turnHandler);
    List<Piece> GetPiecesByType(PieceType pieceType);
    void RemovePiece(Cell cell);
    void MovePiece(Cell fromCell, Cell targetCell);
}