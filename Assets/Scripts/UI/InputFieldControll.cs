using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldControll : MonoBehaviour
{
    [SerializeField] GameObject RunButton, OpenLeaderboardButton,LoadoutButton;
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

        if (IsInputFieldFocused())
        {
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                //Clear selected object
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(RunButton);
            }
            else if (Input.GetAxisRaw("Vertical") == -1)
            {
                //Clear selected object
                EventSystem.current.SetSelectedGameObject(null);

                //set a new selected object
                EventSystem.current.SetSelectedGameObject(OpenLeaderboardButton);
            }
        }
    }

    public bool IsInputFieldFocused()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        return (obj != null && obj.GetComponent<InputField>() != null);
    }
}
