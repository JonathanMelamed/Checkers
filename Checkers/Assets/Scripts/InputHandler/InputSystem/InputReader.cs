using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class InputReader : ScriptableObject
{
    [SerializeField] private GameplayInput gameplayInput;

    public GameplayInput GameplayInput => gameplayInput;

    private void OnEnable()
    {
        if (gameplayInput != null)
        {
            gameplayInput.Enable();
        }
    }

    private void OnDisable()
    {
        if (gameplayInput != null)
        {
            gameplayInput.Disable();
        }
    }
}