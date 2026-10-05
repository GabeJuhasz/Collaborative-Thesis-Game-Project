using Unity.VisualScripting;
using UnityEngine;

public class Module : MonoBehaviour
{
    public string type;
    public float damage;
    public float aps;
    public float progress;
    public float level;
    public float startingAmo;
    public float amo;
    public float reloadTime;
    public float reloadProgress;
    public bool reloading;
    public bool on = true;

    public string myName;

    public GameObject missile;
    public GameObject bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
           progress += Time.deltaTime;
            if (type == "Missile")
            {
                if (progress > aps)
                {
                    progress = 0f;
                    Vector3 spawnPosition = transform.parent.position;
                    Instantiate(missile, spawnPosition, Quaternion.identity);
                    //GameObject newChild = Instantiate(missile, spawnPosition, Quaternion.identity);
                }
            } 

            if (type == "Bullet" && !reloading)
            {
                if (progress > aps)
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
}
