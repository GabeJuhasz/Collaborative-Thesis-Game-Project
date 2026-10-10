using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static bool[] slots = new bool[35] {false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckFullness();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckFullness()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            
        }
    }

        public int FindFirstEmpty()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == true)
            {
                return i + 1;
            }     
        }
        return 0;
    }
}
