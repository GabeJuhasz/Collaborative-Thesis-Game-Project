using UnityEngine;
using UnityEngine.EventSystems;

public class HoverAPlanet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int planetNumber;
    public ShipOrbit orbit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!orbit.going)
        {
            if (planetNumber == 1)
            {
                orbit.call2 = true;
            }

            if (planetNumber == 2)
            {
                orbit.call1 = true;
            }

            if (planetNumber == 3)
            {
                orbit.call3 = true;
            }

            if (planetNumber == 4)
            {
                orbit.call4 = true;
            }

            if (planetNumber == 5)
            {
                orbit.call5 = true;
            }
        }

        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
