using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public float damage;

    public float spawnSpeed;
    public float progress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        Vector3 spawnPosition = transform.position;
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        newEnemy.GetComponent<EnemyStats>().damage = damage;
    }
}
