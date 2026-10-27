using Unity.VisualScripting;
using UnityEngine;

public class Module : MonoBehaviour
{
    public string type;
    public float damage;
    public float aps;
    public float level;
    public float startingAmo;
    public float reloadTime;
    
    public float amo;
    public float reloadProgress;
    public bool reloading;
    public bool on = true;
    public float progress;
    public string myName;

    public GameObject missile;
    public GameObject bullet;

    public float damageAugment;
    public float amoAugment;
    public float fireRateAugment;
    public float upgradeCostAugment;

    public float maxLv;

    public int slotNum;

    public string rarity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
           progress += Time.deltaTime;
            if (type == "Missile")
            {
                if (progress > 1/aps)
                {
                    progress = 0f;
                    Vector3 spawnPosition = transform.parent.position;
                    Instantiate(missile, spawnPosition, Quaternion.identity);
                    //GameObject newChild = Instantiate(missile, spawnPosition, Quaternion.identity);
                }
            } 

            if (type == "Bullet" && !reloading)
            {
                if (progress > 1/aps)
                {
                    progress = 0f;
                    Vector3 spawnPosition = transform.parent.position;
                    GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.identity);
                    newBullet.GetComponent<Bullet>().damage = damage;
                    amo-=1;
                    if (amo < 1)
                    {
                       reloading = true; 
                    }
                }
            }

            if (reloading)
            {
                if (reloadProgress < reloadTime)
                {
                    reloadProgress += Time.deltaTime; 
                }
                else
                {
                    amo = startingAmo;
                    reloading = false;
                    reloadProgress = 0;
                    
                }
                
            } 
        }
    }

    public void RefreshStatHolder()
    {
        AllModStatsHolder.type[slotNum - 1] = type;
        AllModStatsHolder.damage[slotNum - 1] = damage;
        AllModStatsHolder.aps[slotNum - 1] = aps;
        AllModStatsHolder.level[slotNum - 1] = level;
        AllModStatsHolder.reloadTime[slotNum - 1] = reloadTime;
        AllModStatsHolder.amo[slotNum - 1] = amo;
        AllModStatsHolder.myName[slotNum - 1] = myName;
        AllModStatsHolder.upgradeCost[slotNum - 1] = this.gameObject.GetComponent<ModuleUpgrade>().cost;
        AllModStatsHolder.startingAmo[slotNum - 1] = startingAmo;

        AllModStatsHolder.damageUp[slotNum - 1] = damageAugment;
        AllModStatsHolder.amoUp[slotNum - 1] = amoAugment;
        AllModStatsHolder.fireRateUp[slotNum - 1] = fireRateAugment;
        AllModStatsHolder.upgradeCostUp[slotNum - 1] = upgradeCostAugment;

        AllModStatsHolder.maxLv[slotNum - 1] = maxLv;
        AllModStatsHolder.rarity[slotNum - 1] = rarity;
        
    }
}
