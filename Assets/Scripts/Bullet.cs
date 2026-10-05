using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeAlive = 0f;
    public float speed = 10f;
    public GameObject target;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        
        if(enemies.Length == 0)
        {
            return;
        }

        target = enemies[0];
        
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(enemies[i].transform.position, transform.position) < Vector3.Distance(target.transform.position, transform.position))
            {
                 target = enemies[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > 3f)
        {
            Destroy(gameObject);
        }

        if (target == null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(target.transform.position);

            if (Vector3.Distance(target.transform.position, transform.position) < .1f )
            {
                target.GetComponent<EnemyStats>().hp -= damage;
                Destroy(gameObject);
            }
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
