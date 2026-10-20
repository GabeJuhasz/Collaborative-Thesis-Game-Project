using UnityEngine;

public class EnemyDropMod : MonoBehaviour
{
    public static bool rareMissileGot = false;
    public static bool rareBulletGot = false;

    public static bool epicMissileGot = false;
    public static bool epicBulletGot = false;

    public static bool legendaryMissileGot = false;
    public static bool legendaryBulletGot = false;

    public static int itemsToAddC = 0;
    public static int itemsToAddR = 0;
    public static int itemsToAddE = 0;
    public static int itemsToAddL = 0;

    public GameObject commonDrop;
    public GameObject rareDrop;
    public GameObject epicDrop;
    public GameObject legendaryDrop;

    public InventoryManager iM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        iM = GameObject.Find("ModTracker").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropItem()
    {
        string rarity = "c";
       if( iM.FindFirstEmpty() == 0)
        {
            DisplayFullMessage();
        }
        else
        {
            float dropChance = Random.Range(0,100);
            if (dropChance < 40f)
            {
                return;
            }
            
            if (dropChance < 60f && dropChance > 40f)
            {
                itemsToAddC ++;
                rarity = "c";
                InventoryManager.slots[iM.FindFirstEmpty() - 1] = true;
            }
            
            if (dropChance < 85f && dropChance > 60)
            {
                itemsToAddR ++;
                rarity = "r";
                InventoryManager.slots[iM.FindFirstEmpty() - 1] = true;
            }
            
            if (dropChance < 90f && dropChance > 85)
            {
                itemsToAddE ++;
                rarity = "e";
                InventoryManager.slots[iM.FindFirstEmpty() - 1] = true;
            }
            
            if (dropChance < 100f && dropChance > 98f)
            {
                itemsToAddL ++;
                rarity = "l";
                InventoryManager.slots[iM.FindFirstEmpty() - 1] = true;
            }
            
            print(iM.FindFirstEmpty() - 1);
            DisplayItemGot(rarity);
        }
    }

    public void DisplayFullMessage()
    {
        print("Inventory Full");
    }

    public void DisplayItemGot(string rarity)
    {
        Vector3 spawnPosition = transform.position;

        if (rarity == "c")
        {
            GameObject newitemDrop = Instantiate(commonDrop, spawnPosition, Quaternion.identity);
        }

        if (rarity == "r")
        {
            GameObject newitemDrop = Instantiate(rareDrop, spawnPosition, Quaternion.identity);
        }

        if (rarity == "e")
        {
            GameObject newitemDrop = Instantiate(epicDrop, spawnPosition, Quaternion.identity);
        }

        if (rarity == "l")
        {
            GameObject newitemDrop = Instantiate(legendaryDrop, spawnPosition, Quaternion.identity);
        }
    }
}
