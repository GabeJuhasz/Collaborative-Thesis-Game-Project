using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ModuleUpgrade : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text weaponName;
    public TMP_Text level;
    public TMP_Text damage;
    public TMP_Text amo;
    public TMP_Text fireRate;
    public TMP_Text upgradeCost;

    public float cost;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnPointerClick(PointerEventData eventData)
    {
        print("clicked");
        weaponName.text = "" + gameObject.GetComponent<Module>().myName;
        level.text = "Level: " + gameObject.GetComponent<Module>().level;
        damage.text = "Damage: " + gameObject.GetComponent<Module>().damage;
        amo.text = "Amo: " + gameObject.GetComponent<Module>().amo;
        fireRate.text = "Fire Rate: " + gameObject.GetComponent<Module>().aps;
        upgradeCost.text = "Upgrade Cost: " + cost;
    }
}
