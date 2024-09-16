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
            gameplayInput = new GameplayInput();
            gameplayInput.GamePlay.SetCallbacks(this); 
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

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Select action performed");
        }
    }
}