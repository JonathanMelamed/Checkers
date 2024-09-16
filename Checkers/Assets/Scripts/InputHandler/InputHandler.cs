public class InputHandler
{
    private TurnHandler _turnHandler;

    public InputHandler(TurnHandler turnHandler, PlayerInput playerInput)
    {
        _turnHandler = turnHandler;
        playerInput.PieceSelector.OnPieceSelected += _turnHandler.HandlePieceSelected;
        playerInput.CellSelector.OnCellSelected += _turnHandler.HandleCellSelected;
    }
}