using System.Reflection;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Missile : MonoBehaviour
{
    public float startUpTime = 1f;
    public float timeGone = 0f;
    public float timeAlive = 0f;
    public float speed = 10f;
    public GameObject target;
    public GameObject explosion;
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

        print(target.name);

    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > 3f)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(explosion, spawnPosition, Quaternion.identity);
            Destroy(gameObject);
        }


        if (target == null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            if (timeGone < startUpTime)
            {
                timeGone += Time.deltaTime;
                speed = 35;
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                speed = 20f;
                transform.LookAt(target.transform.position);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            if (Vector3.Distance(target.transform.position, transform.position) < .1f )
            {
                Vector3 spawnPosition = transform.position;
                Instantiate(explosion, spawnPosition, Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }
}
