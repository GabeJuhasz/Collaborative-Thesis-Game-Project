using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    public GameObject myMod;
    public static float coins;

     public Button myButton;
    
    public GameObject areYouSureBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSureBox()
    {
        myButton.interactable = false;
        areYouSureBox.SetActive(true);
    }
    public void SellItem()
    {
        if (myMod.GetComponent<Module>().rarity == "c")
        {
            coins += 25 * myMod.GetComponent<Module>().level;
        }

        if (myMod.GetComponent<Module>().rarity == "r")
        {
            coins += 50 * myMod.GetComponent<Module>().level;
        }

        if (myMod.GetComponent<Module>().rarity == "e")
        {
            coins += 100 * myMod.GetComponent<Module>().level;
        }

        if (myMod.GetComponent<Module>().rarity == "l")
        {
            coins += 200 * myMod.GetComponent<Module>().level;
        }

        InventoryManager.slots[myMod.GetComponent<Module>().slotNum -1] = false;
        EnemyDropMod.totalItems--;
        Destroy(myMod);
    }
}
