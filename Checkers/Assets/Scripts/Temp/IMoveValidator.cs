public interface IMoveValidator
{
    bool IsValidRegularMove(Cell sourceCell, Cell targetCell, Piece piece);
    bool IsValidCaptureMove(Cell sourceCell, Cell targetCell, Piece piece);
}