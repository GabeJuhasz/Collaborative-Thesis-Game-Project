using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float hp;

    public float damage;

    public EnemyDropMod dropMod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropMod = this.gameObject.GetComponent<EnemyDropMod>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            dropMod.DropItem();
            Destroy(gameObject);
        }
    }
}
