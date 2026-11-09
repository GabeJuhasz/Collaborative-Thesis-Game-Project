using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public float damage;

    public float spawnSpeed;
    public float progress;

    public float minEnemies = 2f;

    public float sightRange = 30f;

    public float spawnLimit = 5f;
    public float numEnemies = 0f;

    public TMP_Text inOut;

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
        if ((Vector3.Distance(transform.position, player.transform.position) < sightRange/2))
        {
            inOut.text = "In";
        }
        else
        {
            //print("out");
            inOut.text = "out";
        }

        if ((Vector3.Distance(transform.position, player.transform.position) < sightRange/2) || numEnemies < minEnemies)
        {
            //print("In");
            //inOut.text = "In";
            if (numEnemies < spawnLimit)
            {
                Vector3 spawnPosition = transform.position;
                GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
                newEnemy.GetComponent<EnemyStats>().spawner = this;
                newEnemy.GetComponent<EnemyStats>().damage = damage;
                numEnemies++;
            }
        }
        

    }
}
