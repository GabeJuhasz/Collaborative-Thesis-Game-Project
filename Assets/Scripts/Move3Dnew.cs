using UnityEngine;

public class Move3Dnew : MonoBehaviour
{   
    public float rotx = 0f;
    public float roty = 0f;
    public float rotz = 0f;
    public float rotSpeed = 1f;
    public float speed = 1f;
    public float flightSpeed = 1f;
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
        if (Input.GetKey("d"))
        {
            roty += rotSpeed;
        }

        if (Input.GetKey("a"))
        {
            roty -= rotSpeed;
        }

        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }

        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey("space"))
        {
            transform.position = new Vector3(transform.position.x,transform.position.y + speed * Time.deltaTime,transform.position.z);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position = new Vector3(transform.position.x,transform.position.y - speed * Time.deltaTime,transform.position.z);
        }

        transform.rotation = Quaternion.Euler(rotx, roty, rotz);
    }
}
