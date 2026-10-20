using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static bool[] slots = new bool[35] {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public GameObject[] ObjectSlots;

    public GameObject blankMod;

    public ShipModTracker modTracker;

    public TMP_Text weaponNameText;
    public TMP_Text levelText;
    public TMP_Text damageText;
    public TMP_Text amoText;
    public TMP_Text fireRateText;
    public TMP_Text upgradeCostText;

    public Sprite commonBullet;
    public Sprite rareBullet;
    public Sprite epicBullet;
    public Sprite legendaryBullet;

    public Sprite commonMissile;
    public Sprite rareMissile;
    public Sprite epicMissile;
    public Sprite legendaryMissile;

    public GameObject upgradeButton;

    void Start()
    {
        modTracker = GameObject.Find("ModTracker").GetComponent<ShipModTracker>();

        if (EnemyDropMod.itemsToAddC > 0)
        {
            for (int i = 0; i < EnemyDropMod.itemsToAddC; i++)
            {
                EnemyDropMod.itemsToAddC--;
                MakeNewMod("c");
            }
            
            for (int i = 0; i < EnemyDropMod.itemsToAddR; i++)
            {
                EnemyDropMod.itemsToAddC--;
                MakeNewMod("r");
            }

            for (int i = 0; i < EnemyDropMod.itemsToAddE; i++)
            {
                EnemyDropMod.itemsToAddC--;
                MakeNewMod("e");
            }

            for (int i = 0; i < EnemyDropMod.itemsToAddL; i++)
            {
                EnemyDropMod.itemsToAddC--;
                MakeNewMod("l");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            MakeNewMod("c");
        }

        if (Input.GetKeyDown("r"))
        {
            MakeNewMod("r");
        }

        if (Input.GetKeyDown("e"))
        {
            MakeNewMod("e");
        }

        if (Input.GetKeyDown("l"))
        {
            MakeNewMod("l");
        }
    }

    public void CheckFullness()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            
        }
    }

        public int FindFirstEmpty()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == false)
            {
                return i + 1;
            }     
        }
        return 0;
    }

    public void MakeNewMod(string rarity)
    {   
        int typeNum = Random.Range(0,2);
        string type = "";
        string name = "";
        float damage = 0;
        float amo = 0;
        float atkSpeed = 0;
        float reloadSpeed = 0;

        if (typeNum == 0)
        {
            type = "Bullet";
            name = "Machine Gun";
            damage = 1;
            amo = 10;
            atkSpeed = 0.5f;
            reloadSpeed = 0.5f;
        }

        if (typeNum == 1)
        {
            type = "Missile";
            
            name = "Missile Launcher";
            damage = 2;
            amo = 5;
            atkSpeed = 1;
            reloadSpeed = 1;
        }

        GameObject mod = Instantiate(blankMod);
        mod.GetOrAddComponent<DraggableItgem>().tracker = modTracker;
        mod.GetComponent<Module>().type = type;
        mod.GetComponent<Module>().damage = damage;
        mod.GetComponent<Module>().aps = atkSpeed;
        mod.GetComponent<Module>().level = 1;
        mod.GetComponent<Module>().startingAmo = amo;
        mod.GetComponent<Module>().reloadTime = reloadSpeed;
        mod.GetComponent<Module>().on = false;
        mod.GetComponent<Module>().myName = name;

        mod.GetComponent<ModuleUpgrade>().weaponName = weaponNameText;
        mod.GetComponent<ModuleUpgrade>().level = levelText;
        mod.GetComponent<ModuleUpgrade>().damage = damageText;
        mod.GetComponent<ModuleUpgrade>().amo = amoText;
        mod.GetComponent<ModuleUpgrade>().fireRate = fireRateText;
        mod.GetComponent<ModuleUpgrade>().upgradeCost = upgradeCostText;

        float startingDamageUp = 0f;
        float startingAmoUp = 0f;
        float startingfireRateUp = 0f;
        float startingupgradeCostUp = 0f;

        float damageAugment = 0f;
        float amoAugment = 0f;
        float fireRateAugment = 0f;
        float upgradeCostAugment = 0f;
        if (rarity == "c")
        {
            damageAugment = 1.1f;
            amoAugment = 1.25f;
            fireRateAugment = 1.1f;
            upgradeCostAugment = 1.25f;

            startingDamageUp = damage;
            startingAmoUp = amo;
            startingfireRateUp = atkSpeed;
            startingupgradeCostUp = 100;

            if(type == "Bullet")
            {
                mod.GetComponent<Image>().sprite = commonBullet; 
            }

            if(type == "Missile")
            {
                mod.GetComponent<Image>().sprite = commonMissile;
            }
            
        }
        if (rarity == "r")
        {
            damageAugment = 1.25f;
            amoAugment = 1.5f;
            fireRateAugment = 1.2f;
            upgradeCostAugment = 1.35f;

            int randUp = Random.Range(0,3);
            startingDamageUp = damage + randUp;
            startingAmoUp = amo + randUp;
            startingfireRateUp = atkSpeed + randUp;
            startingupgradeCostUp = 125;
            
            if(type == "Bullet")
            {
                mod.GetComponent<Image>().sprite = rareBullet; 
            }

            if(type == "Missile")
            {
                mod.GetComponent<Image>().sprite = rareMissile;
            }
            
        }
        if (rarity == "e")
        {
            damageAugment = 1.35f;
            amoAugment = 2f;
            fireRateAugment = 1.35f;
            upgradeCostAugment = 1.4f;

            int randUp = Random.Range(3,5);
            startingDamageUp = damage + randUp;
            startingAmoUp = amo + randUp;
            startingfireRateUp = atkSpeed + randUp;
            startingupgradeCostUp = 150;

            if(type == "Missile")
            {
                mod.GetComponent<Image>().sprite = epicMissile;
            }
            
            if(type == "Bullet")
            {
                mod.GetComponent<Image>().sprite = epicBullet; 
            }
            
        }
        if (rarity == "l")
        {
            damageAugment = 1.5f;
            amoAugment = 2.5f;
            fireRateAugment = 1.5f;
            upgradeCostAugment = 1.5f;

            int randUp = Random.Range(5,10);
            startingDamageUp = damage + randUp;
            startingAmoUp = amo + randUp;
            startingfireRateUp = atkSpeed + randUp;
            startingupgradeCostUp = 200;

            if(type == "Missile")
            {
                mod.GetComponent<Image>().sprite = legendaryMissile; 
            }

            if(type == "Bullet")
            {
                mod.GetComponent<Image>().sprite = legendaryBullet; 
            }
           
        }
        mod.GetComponent<ModuleUpgrade>().damageUp[0] = startingDamageUp;
        mod.GetComponent<ModuleUpgrade>().damageUp[1] = startingDamageUp * (damageAugment * 1);
        mod.GetComponent<ModuleUpgrade>().damageUp[2] = startingDamageUp * (damageAugment * 2);
        mod.GetComponent<ModuleUpgrade>().damageUp[3] = startingDamageUp * (damageAugment * 3);
        mod.GetComponent<ModuleUpgrade>().damageUp[4] = startingDamageUp * (damageAugment * 4);

        mod.GetComponent<ModuleUpgrade>().amoUp[0] = startingAmoUp;
        mod.GetComponent<ModuleUpgrade>().amoUp[1] = startingAmoUp * (amoAugment * 1);
        mod.GetComponent<ModuleUpgrade>().amoUp[2] = startingAmoUp * (amoAugment * 2);
        mod.GetComponent<ModuleUpgrade>().amoUp[3] = startingAmoUp * (amoAugment * 3);
        mod.GetComponent<ModuleUpgrade>().amoUp[4] = startingAmoUp * (amoAugment * 4);

        mod.GetComponent<ModuleUpgrade>().fireRateUp[0] = startingfireRateUp;
        mod.GetComponent<ModuleUpgrade>().fireRateUp[1] = startingfireRateUp * (fireRateAugment * 1);
        mod.GetComponent<ModuleUpgrade>().fireRateUp[2] = startingfireRateUp * (fireRateAugment * 2);
        mod.GetComponent<ModuleUpgrade>().fireRateUp[3] = startingfireRateUp * (fireRateAugment * 3);
        mod.GetComponent<ModuleUpgrade>().fireRateUp[4] = startingfireRateUp * (fireRateAugment * 4);

        mod.GetComponent<ModuleUpgrade>().upgradeCostUp[0] = startingupgradeCostUp;
        mod.GetComponent<ModuleUpgrade>().upgradeCostUp[1] = startingupgradeCostUp * (upgradeCostAugment * 1);
        mod.GetComponent<ModuleUpgrade>().upgradeCostUp[2] = startingupgradeCostUp * (upgradeCostAugment * 2);
        mod.GetComponent<ModuleUpgrade>().upgradeCostUp[3] = startingupgradeCostUp * (upgradeCostAugment * 3);
        mod.GetComponent<ModuleUpgrade>().upgradeCostUp[4] = startingupgradeCostUp * (upgradeCostAugment * 4);

        mod.GetComponent<ModuleUpgrade>().upgradeButton = upgradeButton;

        mod.GetComponent<ModuleUpgrade>().cost = startingupgradeCostUp;

        mod.transform.SetParent(ObjectSlots[FindFirstEmpty() - 1].transform);
        slots[FindFirstEmpty()-1] = true;

    }
}
