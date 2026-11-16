using UnityEngine;
using TMPro;

public class UpgradeShield : MonoBehaviour
{
    public TMP_Text shieldCost;
    public TMP_Text shieldAmount;

    public float cost;
    public float amount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shieldCost.text = "Cost: " + cost;
        shieldAmount.text = amount + "/s Shield";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        if (cost < SellButton.coins)
        {
            SellButton.coins -= cost;
            cost += 125f;
            amount += 1f;
        }
        shieldCost.text = "Cost: " + cost;
        shieldAmount.text = amount + "/s Shield";
    }
}
