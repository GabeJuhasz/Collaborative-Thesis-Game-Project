using UnityEngine;

public class ItemDropGoesToPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
        {
            transform.LookAt(player.transform.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            speed += .01f;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
