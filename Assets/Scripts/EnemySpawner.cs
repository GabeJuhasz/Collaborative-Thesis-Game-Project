using UnityEngine;
//using TMPro;

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

    public bool tower;

    //public TMP_Text inOut;

    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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

        if (tower && enemiesKilled >= maxEnemies)
        {
            floorZone.SetActive(false);
            floorZoneDone.SetActive(true);
        }
        

    }
}
