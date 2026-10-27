using UnityEngine;

public class ShipModLoader : MonoBehaviour
{
    public GameObject missileLauncher;
    public GameObject machineGun;

    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;
    public GameObject slot6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuildModsInLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildModsInLevel()
    {
        Vector3 spawnPosition = new Vector3(0f,0f,0f);
        for(int i = 0; i < 6; i++)
        {
            if (InventoryManager.slots[35 + i] == true)
            {
                if (AllModStatsHolder.type[35 + i] == "Bullet")
                {
                    if (i == 0)spawnPosition = slot1.transform.position; 
        
                    if (i == 1)spawnPosition = slot2.transform.position; 
                    
                    if (i == 2)spawnPosition = slot3.transform.position;
                    
                    if (i == 3)spawnPosition = slot4.transform.position;
                    
                    if (i == 4)spawnPosition = slot5.transform.position;
                    
                    if (i == 5)spawnPosition = slot6.transform.position;
                    
                    GameObject weapon = Instantiate(machineGun, spawnPosition, Quaternion.identity);
                    
                    if (i == 0)
                    {
                        weapon.transform.SetParent(slot1.transform);  
                    }
                    if (i == 1)
                    {
                        weapon.transform.SetParent(slot2.transform);  
                    }
                    if (i == 2)
                    {
                        weapon.transform.SetParent(slot3.transform);  
                    }
                    if (i == 3)
                    {
                        weapon.transform.SetParent(slot4.transform);  
                    }
                    if (i == 4)
                    {
                        weapon.transform.SetParent(slot5.transform);  
                    }
                    if (i == 5)
                    {
                        weapon.transform.SetParent(slot6.transform); 
                    }
                    weapon.transform.GetChild(0).GetComponent<Module>().myName = AllModStatsHolder.myName[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().type = AllModStatsHolder.type[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().damage = AllModStatsHolder.damage[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().aps = AllModStatsHolder.aps[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().level = AllModStatsHolder.level[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().reloadTime = AllModStatsHolder.reloadTime[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().amo = AllModStatsHolder.amo[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().myName = AllModStatsHolder.myName[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().startingAmo = AllModStatsHolder.startingAmo[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().rarity = AllModStatsHolder.rarity[35 + i];
                }

                if (AllModStatsHolder.type[35 + i] == "Missile")
                {
                    if (i == 0)spawnPosition = slot1.transform.position; 
        
                    if (i == 1)spawnPosition = slot2.transform.position; 
                    
                    if (i == 2)spawnPosition = slot3.transform.position;
                    
                    if (i == 3)spawnPosition = slot4.transform.position;
                    
                    if (i == 4)spawnPosition = slot5.transform.position;
                    
                    if (i == 5)spawnPosition = slot6.transform.position;
                    GameObject weapon = Instantiate(missileLauncher, spawnPosition, Quaternion.identity);
                    
                    if (i == 0)
                    {
                        weapon.transform.SetParent(slot1.transform);  
                    }
                    if (i == 1)
                    {
                        weapon.transform.SetParent(slot2.transform);  
                    }
                    if (i == 2)
                    {
                        weapon.transform.SetParent(slot3.transform);  
                    }
                    if (i == 3)
                    {
                        weapon.transform.SetParent(slot4.transform);  
                    }
                    if (i == 4)
                    {
                        weapon.transform.SetParent(slot5.transform);  
                    }
                    if (i == 5)
                    {
                        weapon.transform.SetParent(slot6.transform); 
                    }
                    weapon.transform.GetChild(0).GetComponent<Module>().myName = AllModStatsHolder.myName[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().type = AllModStatsHolder.type[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().damage = AllModStatsHolder.damage[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().aps = AllModStatsHolder.aps[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().level = AllModStatsHolder.level[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().reloadTime = AllModStatsHolder.reloadTime[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().amo = AllModStatsHolder.amo[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().myName = AllModStatsHolder.myName[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().startingAmo = AllModStatsHolder.startingAmo[35 + i];
                    weapon.transform.GetChild(0).GetComponent<Module>().rarity = AllModStatsHolder.rarity[35 + i];
                }
            }
            
            
        }
    }
}

