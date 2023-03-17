using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ShopCharacterList : ShopList
{
    [SerializeField] GameObject CharacterListButton;

    public override void Populate()
    {
        m_RefreshCallback = null;
        foreach (Transform t in listRoot)
        {
            Destroy(t.gameObject);
        }

        foreach (KeyValuePair<string, Character> pair in CharacterDatabase.dictionary)
        {
            Character c = pair.Value;
            if (c != null)
            {
                prefabItem.InstantiateAsync().Completed += (op) =>
                {
                    if (op.Result == null || !(op.Result is GameObject))
                    {
                        Debug.LogWarning(string.Format("Unable to load character shop list {0}.", prefabItem.Asset.name));
                        return;
                    }
                    GameObject newEntry = op.Result;
                    newEntry.transform.SetParent(listRoot, false);

                    ShopItemListItem itm = newEntry.GetComponent<ShopItemListItem>();

                    itm.icon.sprite = c.icon;
                    itm.nameText.text = c.characterName;
                    itm.pricetext.text = c.cost.ToString();

                    itm.buyButton.image.sprite = itm.buyButtonSprite;

                    if (c.premiumCost > 0)
                    {
                        itm.premiumText.transform.parent.gameObject.SetActive(true);
                        itm.premiumText.text = c.premiumCost.ToString();
                    }
                    else
                    {
                        itm.premiumText.transform.parent.gameObject.SetActive(false);
                    }

                    itm.buyButton.onClick.AddListener(delegate () { Buy(c); });

                    m_RefreshCallback += delegate () { RefreshButton(itm, c); };
                    RefreshButton(itm, c);
                };
            }
        }
    }

    protected void RefreshButton(ShopItemListItem itm, Character c)
    {
        if (c.cost > PlayerData.instance.coins)
        {
            itm.buyButton.interactable = false;
            itm.pricetext.color = Color.red;
        }
        else
        {
            itm.pricetext.color = Color.black;
        }

        if (c.premiumCost > PlayerData.instance.premium)
        {
            itm.buyButton.interactable = false;
            itm.premiumText.color = Color.red;
        }
        else
        {
            itm.premiumText.color = Color.black;
        }

        if (PlayerData.instance.characters.Contains(c.characterName))
        {
            itm.buyButton.interactable = false;
            itm.buyButton.image.sprite = itm.disabledButtonSprite;
            itm.buyButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Owned";
            Destroy(itm.buyButton.transform.GetComponent<UnityEngine.UI.Button>());
        }
    }



    public void Buy(Character c)
    {
        PlayerData.instance.coins -= c.cost;
        PlayerData.instance.premium -= c.premiumCost;
        PlayerData.instance.AddCharacter(c.characterName);
        PlayerData.instance.Save();

        // Repopulate to change button accordingly.
        Populate();



        //Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected object
        EventSystem.current.SetSelectedGameObject(CharacterListButton);
    }
}
