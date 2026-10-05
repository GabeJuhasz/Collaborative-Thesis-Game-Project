using UnityEngine;

public class WeaponFaceEnemy : MonoBehaviour
{
    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

        transform.LookAt(target.transform.position);
    }
}
