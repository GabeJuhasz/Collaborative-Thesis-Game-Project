using UnityEngine;

public class ObjectiveCheck : MonoBehaviour
{
    public GameObject endPoint;

    public bool towers;
    public bool rescue;

    public float towersToBreak;
    public float peopleToRescue;

    public float towersBroken;
    public float peopleRescued;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (towers)
        {
            if (towersBroken >= towersToBreak)
            {
                End();
            }
        }

        if (rescue)
        {
            if (peopleRescued >= peopleToRescue)
            {
                End();
            }
        }
    }

    public void End()
    {
        endPoint.SetActive(true);
    }
}
