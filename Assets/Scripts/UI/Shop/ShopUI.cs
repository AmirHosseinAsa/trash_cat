using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.EventSystems;
using System.Collections;

public class ShopUI : MonoBehaviour
{
    public ConsumableDatabase consumableDatabase;

    public ShopItemList itemList;
    public ShopCharacterList characterList;
    public ShopAccessoriesList accessoriesList;
    public ShopThemeList themeList;

    [Header("UI")]
    public Text coinCounter;
    public Text premiumCounter;
    public Button cheatButton;

    protected ShopList m_OpenList;

    protected const int k_CheatCoins = 1000000;
    protected const int k_CheatPremium = 1000;


    void Start()
    {
        PlayerData.Create();

        consumableDatabase.Load();
        CoroutineHandler.StartStaticCoroutine(CharacterDatabase.LoadDatabase());
        CoroutineHandler.StartStaticCoroutine(ThemeDatabase.LoadDatabase());

        m_OpenList = itemList;
        itemList.Open();
        StartCoroutine(SetDownItemListButton());
    }

    IEnumerator SetDownItemListButton()
    {
        yield return new WaitForSeconds(.2f);
        OpenItemList();
    }

    void Update()
    {
        coinCounter.text = PlayerData.instance.coins.ToString();
        premiumCounter.text = PlayerData.instance.premium.ToString();
    }

    public void OpenItemList()
    {
        m_OpenList.Close();
        itemList.Open();
        m_OpenList = itemList;
        SetButtonOnDown("Item");
    }

    public void OpenCharacterList()
    {
        m_OpenList.Close();
        characterList.Open();
        m_OpenList = characterList;
        SetButtonOnDown("Character");
    }

    public void OpenThemeList()
    {
        m_OpenList.Close();
        themeList.Open();
        m_OpenList = themeList;
    }

    public void OpenAccessoriesList()
    {
        m_OpenList.Close();
        accessoriesList.Open();
        m_OpenList = accessoriesList;
    }

    void SetButtonOnDown(string buttonName)
    {
        if (GameObject.FindGameObjectsWithTag("ClaimButton").Any())
        {
            Navigation closeNavigation = GameObject.Find(buttonName).GetComponent<Button>().navigation;
            closeNavigation.mode = Navigation.Mode.Explicit;
            closeNavigation.selectOnDown = GameObject.FindGameObjectsWithTag("ClaimButton").FirstOrDefault().GetComponent<Button>();
            GameObject.Find(buttonName).GetComponent<Button>().navigation = closeNavigation;
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void CloseScene()
    {
        foreach (var button in GameObject.FindGameObjectsWithTag("Button"))
        {
            button.GetComponent<Button>().interactable = true;
        }

        SceneManager.UnloadSceneAsync("shop");
        LoadoutState loadoutState = GameManager.instance.topState as LoadoutState;
        if (loadoutState != null)
        {
            loadoutState.Refresh();
        }
    }

    public void CheatCoin()
    {
        PlayerData.instance.coins += k_CheatCoins;
        PlayerData.instance.premium += k_CheatPremium;
        PlayerData.instance.Save();
    }
}
