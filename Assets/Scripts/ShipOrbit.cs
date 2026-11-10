using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipOrbit : MonoBehaviour
{
    //RectTransform rectTransform;

    public float orbitProgress = 0f;
    public float doomdayCount = 0f;

    public float speed;
    public float turnSpeed;

    public float rotx;
    public float roty;
    public float rotz;

    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    public bool going = false;

    public GameObject lv1;
    public GameObject lv2;
    public GameObject lv3;
    public GameObject lv4;
    public GameObject lv5;

    public bool call1 = false;
    public bool call2 = false;
    public bool call3 = false;
    public bool call4 = false;
    public bool call5 = false;

    public GameObject zone2Stop1;
    public GameObject zone1Stop1;
    public GameObject zone3Stop1;
    public GameObject zone4Stop1;
    public GameObject zone5Stop1;

    private float fastSpeed = -15f;
    private float normalSpeed = -4.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the RectTransform component
        //rectTransform = GetComponent<RectTransform>();

        // Set the Z-axis rotation to 45 degrees
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!going)
        {
            //if(orbitProgress < 360)
            //{
                orbitProgress += Time.deltaTime * turnSpeed;
                //rectTransform.localEulerAngles = new Vector3(0, 0, orbitProgress);
                //transform.rotation = Quaternion.Euler(orbitProgress * rotx, transform.rotation.y, transform.rotation.z);
                transform.rotation = Quaternion.Euler(orbitProgress * rotx, -90f, 90f);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                //transform.position = new Vector3(transform.position.x - xSpeed * Time.deltaTime, transform.position.y - ySpeed * Time.deltaTime, transform.position.z - zSpeed * Time.deltaTime);
            //}
            
        }
        

        if (Input.GetKeyDown("1"))
        {
            //GoToLevelN(1);
            call1 = true;
        }

        if (Input.GetKeyDown("2"))
        {
            //GoToLevelN(2);
            call2 = true;
        }

        if (Input.GetKeyDown("3"))
        {
            //GoToLevelN(2);
            call3 = true;
        }

        if (Input.GetKeyDown("4"))
        {
            //GoToLevelN(2);
            call4 = true;
        }

        if (Input.GetKeyDown("5"))
        {
            //GoToLevelN(2);
            call5 = true;
        }

        //&& (transform.rotation.x < -50f && transform.rotation.x > -63f)
       // if((orbitProgress % 360 < 4))
        //{
            if (call1 )
            {
                print("going");
                GoToLevelN(1);
            }

            if (call2)
            {
                print("going");
                GoToLevelN(2);
            }
        
            if (call3)
            {
                print("going");
                GoToLevelN(3);
            }
            if (call4)
            {
                print("going");
                GoToLevelN(4);
            }
            if (call5)
            {
                print("going");
                GoToLevelN(5);
            }
        //}


        //if (orbitProgress % 360 < 3)
        //{
        //    print("orbitProgress%360: " + orbitProgress % 360);
        //    print("orbitProgress: " + orbitProgress);
        //}

        if (going && call1) 
        {
            if (Vector3.Distance(transform.position, zone2Stop1.transform.position) > .08f)
            {
                transform.Translate(Vector3.forward * -speed * Time.deltaTime);
                speed = fastSpeed;
            }
            else
            {
                going = false;
                call1 = false;
                orbitProgress = 0f;
                speed = normalSpeed;
            }
        }

        if (going && call2)
        {
            if (Vector3.Distance(transform.position, zone1Stop1.transform.position) > .08f)
            {
                transform.Translate(Vector3.forward * -speed * Time.deltaTime);
                speed = fastSpeed;
            }
            else
            {
                going = false;
                call2 = false;
                orbitProgress = 0f;
                speed = normalSpeed;
            }
        }

        if (going && call3)
        {
            if (Vector3.Distance(transform.position, zone3Stop1.transform.position) > .08f)
            {
                transform.Translate(Vector3.forward * -speed * Time.deltaTime);
                speed = fastSpeed;
            }
            else
            {
                going = false;
                call3 = false;
                orbitProgress = 0f;
                speed = normalSpeed;
            }
        }

        if (going && call4)
        {
            if (Vector3.Distance(transform.position, zone4Stop1.transform.position) > .08f)
            {
                transform.Translate(Vector3.forward * -speed * Time.deltaTime);
                speed = fastSpeed;
            }
            else
            {
                going = false;
                call4 = false;
                orbitProgress = 0f;
                speed = normalSpeed;
            }
        }
        
        if (going && call5)
        {
            if (Vector3.Distance(transform.position, zone5Stop1.transform.position) > .08f)
            {
                transform.Translate(Vector3.forward * -speed * Time.deltaTime);
                speed = fastSpeed;
            }
            else
            {
                going = false;
                call5 = false;
                orbitProgress = 0f;
                speed = normalSpeed;
            }
        }
    }

    public void GoToLevelN(int n)
    {
        going = true;

        if (n == 1)
        {
          transform.LookAt(zone2Stop1.transform);  
        }
        
        if (n == 2)
        {
            transform.LookAt(zone1Stop1.transform);
        }

        if (n == 3)
        {
            transform.LookAt(zone3Stop1.transform);
        }

        if (n == 4)
        {
            transform.LookAt(zone4Stop1.transform);
        }

        if (n == 5)
        {
            transform.LookAt(zone5Stop1.transform);
        }

        //transform.rotation = Quaternion.Euler(-145, -90f, 90f);

        //if(Vector3.Distance(transform.position, zone2Stop1.transform.position) > 1f || doomdayCount == 1000)
        //{
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //doomdayCount++;
        //}

    }
}
