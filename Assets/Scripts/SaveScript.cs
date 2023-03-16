using System.Collections;
using UnityEngine;
using XInputDotNetPure;

public class SaveScript : MonoBehaviour
{
    public static bool ChangeMenu = false;
    public static bool IsPopupOpened = false;

    public static bool isVibrationing = false;
    public static PlayerIndex playerIndex;

    void FixedUpdate()
    {
        if (!isVibrationing)
        {
            StopViberation();
        }
    }

    public static IEnumerator Viberation(float duration, float amount)
    {
        isVibrationing = true;
        GamePad.SetVibration(playerIndex, amount, amount);
        yield return new WaitForSeconds(duration);
        isVibrationing = false;
        GamePad.SetVibration(playerIndex, 0, 0);
    }

    public static void StopViberation()
    {
        GamePad.SetVibration(playerIndex, 0, 0);
    }

}
