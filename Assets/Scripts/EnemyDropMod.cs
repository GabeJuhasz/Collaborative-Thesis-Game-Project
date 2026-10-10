using UnityEngine;

public class EnemyDropMod : MonoBehaviour
{
    public static bool rareMissileGot = false;
    public static bool rareBulletGot = false;

    public static bool epicMissileGot = false;
    public static bool epicBulletGot = false;

    public static bool legendaryMissileGot = false;
    public static bool legendaryBulletGot = false;

    public InventoryManager iM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
       if( iM.FindFirstEmpty() == 0)
        {
            DisplayFullMessage();
        }
        else
        {
            float dropChance = Random.Range(0,100);
            DisplayItemGot();
        }
    }

    public void DisplayFullMessage()
    {
        
    }

    public void DisplayItemGot()
    {
        
    }
}
