using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public float damage;

    public float hp;

    public float spawnSpeed;
    public float progress;

    public float minEnemies = 2f;

    public float sightRange = 30f;

    public float spawnLimit = 5f;
    public float numEnemies = 0f;

    public float maxEnemies = 10f;
    public float enemiesKilled = 0f;

    public GameObject floorZone;
    public GameObject floorZoneDone;

    public MissionMarkerHUD missionMarker;

    public TMP_Text missionNumText;

    public bool tower;

    public float towerNum;
    public static float missionNum;

    public bool dead;

    //public TMP_Text inOut;

    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        missionMarker = GameObject.FindAnyObjectByType<MissionMarkerHUD>();
        missionNum = 1;

        if (tower)
        {
            missionNumText = GameObject.Find("Mission Tower Num").GetComponent<TMP_Text>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (progress < spawnSpeed)
        {
            progress += Time.deltaTime;
        }
        else
        {
            progress = 0f;
            SpawnEnemeies();
        }

        if (Input.GetKeyDown("k"))
        {
            print(missionNum);
        }

        if (tower)
        {
            if (tower && enemiesKilled >= maxEnemies && !dead)
            {
                dead = true;
                floorZone.SetActive(false);
                floorZoneDone.SetActive(true);

                if (missionNum < 11)
                {
                    missionNum = towerNum + 1f;
                }

            }

            missionNumText.text = "Destroy Tower " + missionNum;
            if (missionNum == 1)
            {
                if (towerNum == 1)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 2)
            {
                if (towerNum == 2)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 3)
            {
                if (towerNum == 3)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 4)
            {
                if (towerNum == 4)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 5)
            {
                if (towerNum == 5)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 6)
            {
                if (towerNum == 6)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 7)
            {
                if (towerNum == 7)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 8)
            {
                if (towerNum == 8)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 9)
            {
                if (towerNum == 9)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 10)
            {
                if (towerNum == 10)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }

            if (missionNum == 11)
            {
                if (towerNum == 11)
                {
                    missionMarker.missionPoint = floorZone.gameObject.transform;
                }
            }
        }
        
    }

    public void SpawnEnemeies()
    {
        //if ((Vector3.Distance(transform.position, player.transform.position) < sightRange/2))
        //{
            //inOut.text = "In";
        //}
        //else
        //{
            //print("out");
            //inOut.text = "out";
        //}

        if (((Vector3.Distance(transform.position, player.transform.position) < sightRange/2) || numEnemies < minEnemies) && enemiesKilled <= maxEnemies)
        {
            //print("In");
            //inOut.text = "In";
            if (numEnemies < spawnLimit)
            {
                Vector3 spawnPosition = transform.position;
                spawnPosition.x = spawnPosition.x + Random.Range(-6,12);
                spawnPosition.y = spawnPosition.y + Random.Range(1,12);
                spawnPosition.z = spawnPosition.z + Random.Range(-6,12);
                GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
                newEnemy.GetComponent<EnemyStats>().spawner = this;
                newEnemy.GetComponent<EnemyAI>().spawner = this;
                newEnemy.GetComponent<EnemyStats>().damage = damage;
                 newEnemy.GetComponent<EnemyStats>().hp = hp;
                numEnemies++;
            }
        }

       // if (tower && enemiesKilled >= maxEnemies)
       // {
          //  floorZone.SetActive(false);
          //  floorZoneDone.SetActive(true);

           // if(missionNum < 11)
           // {
           //     missionNum = towerNum + 1f;
           // }
            
        //}
        

    }
}
