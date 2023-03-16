using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldControll : MonoBehaviour
{
    [SerializeField] GameObject RunButton, OpenLeaderboardButton, LoadoutButton;
    void Update()
    {
        if (SaveScript.IsPopupOpened)
        {
            RunButton.GetComponent<Button>().interactable = false;
            OpenLeaderboardButton.GetComponent<Button>().interactable = false;
            LoadoutButton.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<InputField>().interactable = false;
        }
        else
        {
            RunButton.GetComponent<Button>().interactable = true;
            OpenLeaderboardButton.GetComponent<Button>().interactable = true;
            LoadoutButton.GetComponent<Button>().interactable = true;
            gameObject.GetComponent<InputField>().interactable = true;
        }
    }

    public bool IsInputFieldFocused()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        return (obj != null && obj.GetComponent<InputField>() != null);
    }
}
