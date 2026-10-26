using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ModUpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TMP_Text weaponName;
    public TMP_Text level;
    public TMP_Text damage;
    public TMP_Text amo;
    public TMP_Text fireRate;
    public TMP_Text upgradeCost;

    public GameObject myMod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myMod.GetComponent<ModuleUpgrade>().UpgradeCount < myMod.GetComponent<Module>().maxLv)
        {
            //weaponName.text = "" + myMod.GetComponent<Module>().myName;
            //level.text = "Level: " + myMod.GetComponent<Module>().level + " -> " + (myMod.GetComponent<Module>().level + 1);
            //damage.text = "Damage: " + myMod.GetComponent<Module>().damage + " -> " + myMod.GetComponent<ModuleUpgrade>().damageUp[(int)myMod.GetComponent<ModuleUpgrade>().UpgradeCount +1];
            //amo.text = "Amo: " + myMod.GetComponent<Module>().amo + "-> " + myMod.GetComponent<ModuleUpgrade>().amoUp[(int)myMod.GetComponent<ModuleUpgrade>().UpgradeCount +1];
            //fireRate.text = "Fire Rate: " + myMod.GetComponent<Module>().aps + " -> " + myMod.GetComponent<ModuleUpgrade>().fireRateUp[(int)myMod.GetComponent<ModuleUpgrade>().UpgradeCount +1];
            //upgradeCost.text = "Upgrade Cost: " + myMod.GetComponent<ModuleUpgrade>().cost + " -> " + myMod.GetComponent<ModuleUpgrade>().upgradeCostUp[(int)myMod.GetComponent<ModuleUpgrade>().UpgradeCount +1]; 

            weaponName.text = "" + myMod.GetComponent<Module>().myName;
            level.text = "Level: " + myMod.GetComponent<Module>().level + " -> " + (myMod.GetComponent<Module>().level + 1);
            damage.text = "Damage: " + myMod.GetComponent<Module>().damage + " -> " + myMod.GetComponent<Module>().damage * myMod.GetComponent<Module>().damageAugment * myMod.GetComponent<Module>().level;
            amo.text = "Amo: " + myMod.GetComponent<Module>().amo + "-> " + myMod.GetComponent<Module>().amo * myMod.GetComponent<Module>().amoAugment * myMod.GetComponent<Module>().level;
            fireRate.text = "Fire Rate: " + myMod.GetComponent<Module>().aps + " -> " + myMod.GetComponent<Module>().aps * myMod.GetComponent<Module>().fireRateAugment * myMod.GetComponent<Module>().level;
            upgradeCost.text = "Upgrade Cost: " + myMod.GetComponent<ModuleUpgrade>().cost + " -> " + myMod.GetComponent<ModuleUpgrade>().cost * myMod.GetComponent<Module>().upgradeCostAugment * myMod.GetComponent<Module>().level;
        }
        else
        {
            level.text = "Level: " + myMod.GetComponent<Module>().level + " -> Max Lv";
            damage.text = "Damage: " + myMod.GetComponent<Module>().damage;
            amo.text = "Amo: " + myMod.GetComponent<Module>().amo;
            fireRate.text = "Fire Rate: " + myMod.GetComponent<Module>().aps;
            upgradeCost.text = "Upgrade Cost: " + myMod.GetComponent<ModuleUpgrade>().cost;
        }


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        weaponName.text = "" + myMod.GetComponent<Module>().myName;
        level.text = "Level: " + myMod.GetComponent<Module>().level;
        damage.text = "Damage: " + myMod.GetComponent<Module>().damage;
        amo.text = "Amo: " + myMod.GetComponent<Module>().amo;
        fireRate.text = "Fire Rate: " + myMod.GetComponent<Module>().aps;
        upgradeCost.text = "Upgrade Cost: " + myMod.GetComponent<ModuleUpgrade>().cost;
    }

    public void UpgradeMod()
    {
        if (myMod.GetComponent<ModuleUpgrade>().UpgradeCount < myMod.GetComponent<Module>().maxLv)
        {
            myMod.GetComponent<ModuleUpgrade>().UpgradeCount++;
            myMod.GetComponent<Module>().level++;
            myMod.GetComponent<Module>().damage *= myMod.GetComponent<Module>().damageAugment * myMod.GetComponent<Module>().level;
            myMod.GetComponent<Module>().amo *= myMod.GetComponent<Module>().amoAugment * myMod.GetComponent<Module>().level;
            myMod.GetComponent<ModuleUpgrade>().cost *= myMod.GetComponent<Module>().upgradeCostAugment * myMod.GetComponent<Module>().level;
            myMod.GetComponent<Module>().aps *= myMod.GetComponent<Module>().fireRateAugment * myMod.GetComponent<Module>().level;
            myMod.GetComponent<Module>().RefreshStatHolder();

        }
        else
        {
            level.text = "Level: " + myMod.GetComponent<Module>().level + " -> Max Lv";
            damage.text = "Damage: " + myMod.GetComponent<Module>().damage;
            amo.text = "Amo: " + myMod.GetComponent<Module>().amo;
            fireRate.text = "Fire Rate: " + myMod.GetComponent<Module>().aps;
            upgradeCost.text = "Upgrade Cost: " + myMod.GetComponent<ModuleUpgrade>().cost;
        }
           
    }
}
