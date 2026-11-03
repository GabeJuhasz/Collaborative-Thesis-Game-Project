using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SellButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject myMod;
    public static float coins;
    public float sellValue;
    public TMP_Text myCoins;
    public TMP_Text sellCoins;

     public Button myButton;

     public GameObject upgradeButton;
    
    public GameObject areYouSureBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myCoins.text = "Coins: " + coins;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void Cancel()
    {
        myButton.interactable = true;
        areYouSureBox.SetActive(false);
    }

    public void OpenSureBox()
    {
        if (upgradeButton.GetComponent<ModUpgradeButton>().myMod != null)
        {
        myButton.interactable = false;
        areYouSureBox.SetActive(true);
        myMod = upgradeButton.GetComponent<ModUpgradeButton>().myMod;
        FindSellValue();
        sellCoins.text = "You will get " + sellValue + " coins.";
        }

    }

    public void FindSellValue()
    {
        if (myMod.GetComponent<Module>().rarity == "c")
        {
            sellValue= 25 * myMod.GetComponent<Module>().level;
        }

        if (myMod.GetComponent<Module>().rarity == "r")
        {
            sellValue= 50 * myMod.GetComponent<Module>().level;
        }

        if (myMod.GetComponent<Module>().rarity == "e")
        {
            sellValue= 100 * myMod.GetComponent<Module>().level;
        }

        if (myMod.GetComponent<Module>().rarity == "l")
        {
            sellValue= 200 * myMod.GetComponent<Module>().level;
        }
    }
    public void SellItem()
    {
        coins += sellValue;
        InventoryManager.slots[myMod.GetComponent<Module>().slotNum -1] = false;
        EnemyDropMod.totalItems--;
        upgradeButton.GetComponent<ModUpgradeButton>().myMod = null;
        Destroy(myMod);
        myButton.interactable = true;
        areYouSureBox.SetActive(false);
    }
}
