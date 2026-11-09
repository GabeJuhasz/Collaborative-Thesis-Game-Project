using UnityEngine;

public class RangeSize : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public EnemySpawner spawner;
    public EnemyAI stats;
    public float range;
    void Start()
    {
        if (transform.parent.CompareTag("Enemy") || transform.parent.CompareTag("Spawner"))
        {
            if (transform.parent.CompareTag("Spawner"))
            {
                spawner = transform.parent.GetComponent<EnemySpawner>();
                GetComponent<SphereCollider>().radius = spawner.sightRange;
                range = spawner.sightRange;
            }

            if (transform.parent.CompareTag("Enemy"))
            {
                stats = transform.parent.GetComponent<EnemyAI>();
                GetComponent<SphereCollider>().radius = stats.sightRange;
                range = stats.sightRange;
            }
            print(transform.parent.name + " Range: " + range);
        }
    }

    // Update is called once per frame
    void Update()
    {
            if (transform.parent.CompareTag("Spawner"))    
            {
                spawner = transform.parent.GetComponent<EnemySpawner>();
                //this.gameObject.GetComponent<SphereCollider>().radius = range;
                this.gameObject.transform.localScale = new Vector3(range, range, range);
        }
            
            if (transform.parent.CompareTag("Enemy"))
            {
                stats = transform.parent.GetComponent<EnemyAI>();
                //this.gameObject.GetComponent<SphereCollider>().radius = range;
                this.gameObject.transform.localScale = new Vector3(range, range, range);
        }
    }
}
