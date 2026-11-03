using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject player;
    public float speed = 10f;

    public float lifeTime = 3f;
    public float timeAlive = 0f;
    public float damage = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timeAlive += Time.deltaTime;
        if (timeAlive > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "HitBox")
        {
            if (HpAndShieldBars.shield > 0f)
            {
                HpAndShieldBars.shield -= damage;  
            }
            else
            {
                HpAndShieldBars.hp -= damage;
            }
           Destroy(gameObject);
        }
        
    }


}
