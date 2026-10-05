using UnityEngine;

public class Move3D : MonoBehaviour
{
    public float rotx = 0f;
    public float roty = 0f;
    public float rotz = 0f;
    public float rotSpeed = 1f;
    public float speed = 1f;

    public GameObject missile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotx = 0f;
        roty = 0f;
        rotz = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 50)
        {
            transform.position = new Vector3(-50,transform.position.y,transform.position.z);
        }

                if (transform.position.z >= 50)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,-50);
        }


        if (Input.GetKey("s"))
        {
            rotx += rotSpeed;
        }

        if (Input.GetKey("w"))
        {
            rotx -= rotSpeed;
        }

        if (Input.GetKey("d"))
        {
            roty += rotSpeed;
        }

        if (Input.GetKey("a"))
        {
            roty -= rotSpeed;
        }

        if (Input.GetKey("q"))
        {
            rotz += rotSpeed;
        }

        if (Input.GetKey("e"))
        {
            rotz -= rotSpeed;
        }

        if (Input.GetKey("space"))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown("v"))
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(missile, spawnPosition, Quaternion.identity);
        }

        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(rotx, roty, rotz);
    }
}
