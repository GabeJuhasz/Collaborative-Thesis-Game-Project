using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Explosion : MonoBehaviour
{
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Pop", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pop()
    {
        Destroy(gameObject);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(enemies[i].transform.position, transform.position) < 2)
            {
                enemies[i].GetComponent<EnemyStats>().hp -= damage;
            }
        }
    }
}
