using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
public class InputReader : ScriptableObject, GameplayInput.IGamePlayActions
{
    private GameplayInput gameplayInput;

    public GameplayInput GameplayInput => gameplayInput;

    private void OnEnable()
    {
        if (gameplayInput == null)
        {
            gameplayInput = new GameplayInput();  // Initialize the input actions
            gameplayInput.GamePlay.SetCallbacks(this); // Set callbacks to listen for actions
        }
        gameplayInput.Enable();
    }

    private void OnDisable()
    {
        if (gameplayInput != null)
        {
            gameplayInput.Disable();
        }
    }

    // Implement the interface method to handle the "Select" action
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Select action performed");
            // Place logic here for when the "Select" action is triggered
        }
    }
}