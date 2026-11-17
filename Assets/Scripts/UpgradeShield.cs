using UnityEngine;
using TMPro;

public class UpgradeShield : MonoBehaviour
{
    public TMP_Text shieldCost;
    public TMP_Text shieldAmount;

    public static float cost;
    public static float amount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        amount = 1f;
        cost = 100f;
        shieldCost.text = "Cost: " + cost;
        shieldAmount.text = amount + "/s Shield";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            //shield = 50f;
            SellButton.coins += 1000;
        }
    }

    public void Upgrade()
    {
        if (cost < SellButton.coins)
        {
            SellButton.coins -= cost;
            cost += 125f;
            amount += .3f;
        }
        shieldCost.text = "Cost: " + cost;
        shieldAmount.text = amount + "/s Shield";
    }
}
