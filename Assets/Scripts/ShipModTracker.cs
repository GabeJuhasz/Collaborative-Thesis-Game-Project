using UnityEngine;

public class ShipModTracker : MonoBehaviour
{
    public static string type0;
    public static  float damage0;
    public static  float aps0;
    public static  float level0;
    public static  float startingAmo0;
    public static  float reloadTime0;

    public static string type1;
    public static  float damage1;
    public static  float aps1;
    public static  float level1;
    public static  float startingAmo1;
    public static  float reloadTime1;

    public static string type2;
    public static  float damage2;
    public static  float aps2;
    public static  float level2;
    public static  float startingAmo2;
    public static  float reloadTime2;

    public static string type3;
    public static  float damage3;
    public static  float aps3;
    public static  float level3;
    public static  float startingAmo3;
    public static  float reloadTime3;

    public static string type4;
    public static  float damage4;
    public static  float aps4;
    public static  float level4;
    public static  float startingAmo4;
    public static  float reloadTime4;

    public static string type5;
    public static  float damage5;
    public static  float aps5;
    public static  float level5;
    public static  float startingAmo5;
    public static  float reloadTime5;

    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    public GameObject slot6;

    public GameObject[] shipmods;

    public GameObject missileLauncher;
    public GameObject machineGun;

    public GameObject ship;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ship != null)
        {
            print("Found Ship");
           LoadWeaponsFromTracker(); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ship == null)
        {
            if (shipmods[0] != null)
        {
            type0 = shipmods[0].GetComponent<Module>().type;
            damage0 = shipmods[0].GetComponent<Module>().damage;
            aps0 = shipmods[0].GetComponent<Module>().aps;
            level0 = shipmods[0].GetComponent<Module>().level;
            startingAmo0 = shipmods[0].GetComponent<Module>().startingAmo;
            reloadTime0 = shipmods[0].GetComponent<Module>().reloadTime;  
        }

        if (shipmods[1] != null)
        {
            type1 = shipmods[1].GetComponent<Module>().type;
            damage1 = shipmods[1].GetComponent<Module>().damage;
            aps1 = shipmods[1].GetComponent<Module>().aps;
            level1 = shipmods[1].GetComponent<Module>().level;
            startingAmo1 = shipmods[1].GetComponent<Module>().startingAmo;
            reloadTime1 = shipmods[1].GetComponent<Module>().reloadTime;  
        }

        if (shipmods[2] != null)
        {
            type2 = shipmods[2].GetComponent<Module>().type;
            damage2 = shipmods[2].GetComponent<Module>().damage;
            aps2 = shipmods[2].GetComponent<Module>().aps;
            level2 = shipmods[2].GetComponent<Module>().level;
            startingAmo2 = shipmods[2].GetComponent<Module>().startingAmo;
            reloadTime2 = shipmods[2].GetComponent<Module>().reloadTime;  
        }

        if (shipmods[3] != null)
        {
            type3 = shipmods[3].GetComponent<Module>().type;
            damage3 = shipmods[3].GetComponent<Module>().damage;
            aps3 = shipmods[3].GetComponent<Module>().aps;
            level3 = shipmods[3].GetComponent<Module>().level;
            startingAmo3 = shipmods[3].GetComponent<Module>().startingAmo;
            reloadTime3 = shipmods[3].GetComponent<Module>().reloadTime;  
        }

        if (shipmods[4] != null)
        {
            type4 = shipmods[4].GetComponent<Module>().type;
            damage4 = shipmods[4].GetComponent<Module>().damage;
            aps4 = shipmods[4].GetComponent<Module>().aps;
            level4 = shipmods[4].GetComponent<Module>().level;
            startingAmo4 = shipmods[4].GetComponent<Module>().startingAmo;
            reloadTime4 = shipmods[4].GetComponent<Module>().reloadTime;  
        }
        
        if (shipmods[5] != null)
        {
            type5 = shipmods[5].GetComponent<Module>().type;
            damage5 = shipmods[5].GetComponent<Module>().damage;
            aps5 = shipmods[5].GetComponent<Module>().aps;
            level5 = shipmods[5].GetComponent<Module>().level;
            startingAmo5 = shipmods[5].GetComponent<Module>().startingAmo;
            reloadTime5 = shipmods[5].GetComponent<Module>().reloadTime;  
        }
        }

        
    }

    public void LoadWeaponsFromTracker()
    {
        for (int i = 0; i < shipmods.Length;i++)
        {
                print(i);
                
                if (i == 0)
                {
                    if(type0 != "")
                {
                    print(type0);
                    print(type1);
                    print(type2);
                    print(type3);
                    print(type4);
                    print(type5);
                    Vector3 spawnPosition = slot1.transform.position;
                    if (type0 == "Bullet")
                    {
                       GameObject newMacinegun = Instantiate(machineGun, spawnPosition, Quaternion.identity);
                       newMacinegun.transform.SetParent(slot1.transform);
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().damage = damage0;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().aps = aps0;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().level = level0;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo0;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime0;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().on = true;
                       
                    }
                    if (type0 == "Missile")
                    {
                       GameObject newMissileLauncher = Instantiate(missileLauncher, spawnPosition, Quaternion.identity);
                       newMissileLauncher.transform.SetParent(slot1.transform);
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().damage = damage0;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().aps = aps0;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().level = level0;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo0;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime0;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().on = true;
                    }
                }
                }

                if (i == 1)
                {
                    if(type1 != "")
                {
                    Vector3 spawnPosition = slot2.transform.position;
                    if (type1 == "Bullet")
                    {
                       GameObject newMacinegun = Instantiate(machineGun, spawnPosition, Quaternion.identity);
                       newMacinegun.transform.SetParent(slot2.transform);
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().damage = damage1;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().aps = aps1;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().level = level1;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo1;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime1;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().on = true;
                       
                    }
                    if (type1 == "Missile")
                    {
                       GameObject newMissileLauncher = Instantiate(missileLauncher, spawnPosition, Quaternion.identity);
                       newMissileLauncher.transform.SetParent(slot2.transform);
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().damage = damage1;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().aps = aps1;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().level = level1;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo1;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime1;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().on = true;
                    }
                }
                }

                if (i == 2)
                {
                    if(type2 != "")
                {
                    Vector3 spawnPosition = slot3.transform.position;
                    if (type2 == "Bullet")
                    {
                       GameObject newMacinegun = Instantiate(machineGun, spawnPosition, Quaternion.identity);
                       newMacinegun.transform.SetParent(slot3.transform);
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().damage = damage2;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().aps = aps2;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().level = level2;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo2;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime2;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().on = true;
                       
                    }
                    if (type2 == "Missile")
                    {
                       GameObject newMissileLauncher = Instantiate(missileLauncher, spawnPosition, Quaternion.identity);
                       newMissileLauncher.transform.SetParent(slot3.transform);
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().damage = damage2;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().aps = aps2;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().level = level2;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo2;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime2;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().on = true;
                    }
                }
                }

                if (i == 3)
                {
                    if(type3 != "")
                {
                    Vector3 spawnPosition = slot4.transform.position;
                    if (type3 == "Bullet")
                    {
                       GameObject newMacinegun = Instantiate(machineGun, spawnPosition, Quaternion.identity);
                       newMacinegun.transform.SetParent(slot4.transform);
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().damage = damage3;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().aps = aps3;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().level = level3;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo3;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime3;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().on = true;
                       
                    }
                    if (type3 == "Missile")
                    {
                       GameObject newMissileLauncher = Instantiate(missileLauncher, spawnPosition, Quaternion.identity);
                       newMissileLauncher.transform.SetParent(slot4.transform);
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().damage = damage3;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().aps = aps3;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().level = level3;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo3;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime3;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().on = true;
                    }
                }
                }

                if (i == 4)
                {
                    if(type4 != "")
                {
                    Vector3 spawnPosition = slot5.transform.position;
                    if (type4 == "Bullet")
                    {
                       GameObject newMacinegun = Instantiate(machineGun, spawnPosition, Quaternion.identity);
                       newMacinegun.transform.SetParent(slot5.transform);
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().damage = damage4;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().aps = aps4;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().level = level4;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo4;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime4;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().on = true;
                       
                    }
                    if (type4 == "Missile")
                    {
                       GameObject newMissileLauncher = Instantiate(missileLauncher, spawnPosition, Quaternion.identity);
                       newMissileLauncher.transform.SetParent(slot5.transform);
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().damage = damage4;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().aps = aps4;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().level = level4;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo4;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime4;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().on = true;
                    }
                }
                }

                if (i == 5)
                {
                    if(type5 != "")
                {
                    Vector3 spawnPosition = slot6.transform.position;
                    if (type5 == "Bullet")
                    {
                       GameObject newMacinegun = Instantiate(machineGun, spawnPosition, Quaternion.identity);
                       newMacinegun.transform.SetParent(slot6.transform);
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().damage = damage5;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().aps = aps5;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().level = level5;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo5;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime5;
                       newMacinegun.transform.GetChild(0).GetComponent<Module>().on = true;
                       
                    }
                    if (type5 == "Missile")
                    {
                       GameObject newMissileLauncher = Instantiate(missileLauncher, spawnPosition, Quaternion.identity);
                       newMissileLauncher.transform.SetParent(slot6.transform);
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().damage = damage5;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().aps = aps5;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().level = level5;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().startingAmo = startingAmo5;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().reloadTime = reloadTime5;
                       newMissileLauncher.transform.GetChild(0).GetComponent<Module>().on = true;
                    }
                }
                }

        }
    }
}
