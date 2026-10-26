using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        if (SceneManager.GetActiveScene().name == "Inventory")
        {
            for (int i = 0; i < slots.Length;i++)
            {
                if (slots[i] == true)
                {
                    ReloadMod(i);
                }
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

    public void ReloadMod(int i)
    {
        GameObject mod = Instantiate(blankMod);
        mod.transform.SetParent(ObjectSlots[i].transform);

        mod.GetOrAddComponent<DraggableItgem>().tracker = modTracker;
        mod.GetComponent<ModuleUpgrade>().weaponName = weaponNameText;
        mod.GetComponent<ModuleUpgrade>().level = levelText;
        mod.GetComponent<ModuleUpgrade>().damage = damageText;
        mod.GetComponent<ModuleUpgrade>().amo = amoText;
        mod.GetComponent<ModuleUpgrade>().fireRate = fireRateText;
        mod.GetComponent<ModuleUpgrade>().upgradeCost = upgradeCostText;
        mod.GetComponent<Module>().maxLv = 3;

        mod.GetComponent<Module>().myName = AllModStatsHolder.myName[i];
        mod.GetComponent<Module>().type = AllModStatsHolder.type[i];
        mod.GetComponent<Module>().damage = AllModStatsHolder.damage[i];
        mod.GetComponent<Module>().aps = AllModStatsHolder.aps[i];
        mod.GetComponent<Module>().level = AllModStatsHolder.level[i];
        mod.GetComponent<Module>().reloadTime = AllModStatsHolder.reloadTime[i];
        mod.GetComponent<Module>().amo = AllModStatsHolder.amo[i];
        mod.GetComponent<Module>().myName = AllModStatsHolder.myName[i];
        mod.GetComponent<ModuleUpgrade>().cost = AllModStatsHolder.upgradeCost[i];
        mod.GetComponent<Module>().startingAmo = AllModStatsHolder.startingAmo[i];

        mod.GetComponent<Module>().damageAugment = AllModStatsHolder.damageUp[i];
        mod.GetComponent<Module>().amoAugment = AllModStatsHolder.amoUp[i];
        mod.GetComponent<Module>().fireRateAugment = AllModStatsHolder.fireRateUp[i];
        mod.GetComponent<Module>().upgradeCostAugment = AllModStatsHolder.upgradeCostUp[i];

        mod.GetComponent<Module>().maxLv = AllModStatsHolder.maxLv[i];
        mod.GetComponent<Module>().slotNum = i;

        mod.GetComponent<ModuleUpgrade>().upgradeButton = upgradeButton;
       


        if (mod.GetComponent<Module>().rarity == "l")
        {
            if (mod.GetComponent<Module>().type == "Missile")
            {
                mod.GetComponent<Image>().sprite = legendaryMissile;
            }

            if (mod.GetComponent<Module>().type == "Bullet")
            {
                mod.GetComponent<Image>().sprite = legendaryBullet;
            }
        }

        if (mod.GetComponent<Module>().rarity == "e")
        {
            if (mod.GetComponent<Module>().type == "Missile")
            {
                mod.GetComponent<Image>().sprite = epicMissile;
            }

            if (mod.GetComponent<Module>().type == "Bullet")
            {
                mod.GetComponent<Image>().sprite = epicBullet;
            }
        }

        if (mod.GetComponent<Module>().rarity == "r")
        {
            if (mod.GetComponent<Module>().type == "Bullet")
            {
                mod.GetComponent<Image>().sprite = rareBullet;
            }

            if (mod.GetComponent<Module>().type == "Missile")
            {
                mod.GetComponent<Image>().sprite = rareMissile;
            }
        }

        if (mod.GetComponent<Module>().rarity == "c")
        {
            if (mod.GetComponent<Module>().type == "Bullet")
            {
                mod.GetComponent<Image>().sprite = commonBullet;
            }

            if (mod.GetComponent<Module>().type == "Missile")
            {
                mod.GetComponent<Image>().sprite = commonMissile;
            }
        }
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
        mod.GetComponent<Module>().maxLv = 3;

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
            mod.GetComponent<Module>().damageAugment = 1.1f;
            mod.GetComponent<Module>().amoAugment = 1.25f;
            mod.GetComponent<Module>().fireRateAugment = 1.1f;
            mod.GetComponent<Module>().upgradeCostAugment = 1.25f;

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
            mod.GetComponent<Module>().damageAugment = 1.25f;
            mod.GetComponent<Module>().amoAugment = 1.5f;
            mod.GetComponent<Module>().fireRateAugment = 1.2f;
            mod.GetComponent<Module>().upgradeCostAugment = 1.35f;

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
            mod.GetComponent<Module>().damageAugment = 1.35f;
            mod.GetComponent<Module>().amoAugment = 2f;
            mod.GetComponent<Module>().fireRateAugment = 1.35f;
            mod.GetComponent<Module>().upgradeCostAugment = 1.4f;

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
            mod.GetComponent<Module>().damageAugment = 1.5f;
            mod.GetComponent<Module>().amoAugment = 2.5f;
            mod.GetComponent<Module>().fireRateAugment = 1.5f;
            mod.GetComponent<Module>().upgradeCostAugment = 1.5f;

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

        mod.GetComponent<Module>().damage = startingDamageUp;
        mod.GetComponent<Module>().amo = startingAmoUp;
        mod.GetComponent<Module>().aps = startingfireRateUp;
        mod.GetComponent<ModuleUpgrade>().cost = startingupgradeCostUp;

        mod.GetComponent<ModuleUpgrade>().upgradeButton = upgradeButton;

        mod.GetComponent<ModuleUpgrade>().cost = startingupgradeCostUp;
        
        mod.GetComponent<Module>().rarity = rarity;

        mod.transform.SetParent(ObjectSlots[FindFirstEmpty() - 1].transform);
        slots[FindFirstEmpty()-1] = true;

        AllModStatsHolder.type[FindFirstEmpty() - 1] = mod.GetComponent<Module>().type;
        AllModStatsHolder.damage[FindFirstEmpty() - 1] = mod.GetComponent<Module>().damage;
        AllModStatsHolder.aps[FindFirstEmpty() - 1] = mod.GetComponent<Module>().aps;
        AllModStatsHolder.level[FindFirstEmpty() - 1] = mod.GetComponent<Module>().level;
        AllModStatsHolder.reloadTime[FindFirstEmpty() - 1] = mod.GetComponent<Module>().reloadTime;
        AllModStatsHolder.amo[FindFirstEmpty() - 1] = mod.GetComponent<Module>().amo;
        AllModStatsHolder.myName[FindFirstEmpty() - 1] = mod.GetComponent<Module>().myName;
        AllModStatsHolder.upgradeCost[FindFirstEmpty() - 1] = mod.GetComponent<ModuleUpgrade>().cost;
        AllModStatsHolder.startingAmo[FindFirstEmpty() - 1] = mod.GetComponent<Module>().startingAmo;

        AllModStatsHolder.damageUp[FindFirstEmpty() - 1] = mod.GetComponent<Module>().damageAugment;
        AllModStatsHolder.amoUp[FindFirstEmpty() - 1] = mod.GetComponent<Module>().amoAugment;
        AllModStatsHolder.fireRateUp[FindFirstEmpty() - 1] = mod.GetComponent<Module>().fireRateAugment;
        AllModStatsHolder.upgradeCostUp[FindFirstEmpty() - 1] = mod.GetComponent<Module>().upgradeCostAugment;

        AllModStatsHolder.maxLv[FindFirstEmpty() - 1] = mod.GetComponent<Module>().maxLv;
        AllModStatsHolder.slotNum[FindFirstEmpty() - 1] = FindFirstEmpty() - 1;

        AllModStatsHolder.rarity[FindFirstEmpty() - 1] = rarity;



    }
}
