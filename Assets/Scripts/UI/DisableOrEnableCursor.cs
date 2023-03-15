using UnityEngine;

public class DisableOrEnableCursor : MonoBehaviour
{
    void Update()
    {
        Cursor.lockState = GameState.isPaused || !GameState.isStarted ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = GameState.isPaused || !GameState.isStarted;
    }
}
