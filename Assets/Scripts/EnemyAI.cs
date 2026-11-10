using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public float minDistance = 5f;
    public GameObject laser;

    public float fireRate = 1f;
    public float progress = 0f;

    public float sightRange = 20f;

    public EnemySpawner spawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < sightRange/2)
        {
            progress += Time.deltaTime;

                if (progress > fireRate)
                {
                    progress = 0f;
                    Vector3 spawnPosition = gameObject.transform.position;
                    GameObject weapon = Instantiate(laser, spawnPosition, Quaternion.identity);
                }

                if (Vector3.Distance(transform.position, player.transform.position) > minDistance)
                {
                    transform.LookAt(player.transform.position);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
        }

        if (Vector3.Distance(transform.position, player.transform.position) > sightRange / 2 && Vector3.Distance(transform.position, spawner.transform.position) > spawner.sightRange/3)
        {
            transform.LookAt(spawner.transform.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }


    }
}
