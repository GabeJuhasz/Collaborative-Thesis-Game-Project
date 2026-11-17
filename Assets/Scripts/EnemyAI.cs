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

    public bool rammer = false;

    public float ramProgress;
    public float ramTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         player = GameObject.FindWithTag("Player");
        if (rammer)
        {
            minDistance = .01f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (rammer)
        //{
        //    ramProgress += Time.deltaTime;
        //}
        
        if (Vector3.Distance(transform.position, player.transform.position) < sightRange/2)
        {
            progress += Time.deltaTime;
            //ramProgress += Time.deltaTime;

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

                //if (rammer && ramProgress < ramTime && ramProgress > 0f)
                //{
                //    transform.LookAt(player.transform.position);
                //    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                //    speed = 10;
                //}

                //if ( ramProgress > ramTime)
                //{
                //    ramProgress = -1f;
                //    speed = 5;
                //}

                if (Vector3.Distance(transform.position, player.transform.position) < .1f && rammer)
                {
                    RamPlayer(5);
                }
        }

        if (!rammer)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > sightRange / 2 && Vector3.Distance(transform.position, spawner.transform.position) > spawner.sightRange/3 && !rammer)
            {
                transform.LookAt(spawner.transform.position);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
       


    }

    public void RamPlayer(float damage)
    {
        
    }
}
