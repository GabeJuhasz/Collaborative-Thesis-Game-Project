using UnityEngine;
using TMPro;

public class HpAndShieldBars : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text shieldText;

    public static float hp;
    public static float shield;

    public float maxHp = 100;
    public float maxShiled = 100;

    public RectTransform hpBar;
    public RectTransform shiledBar;

    public GameObject shiledObject;

    public float slider;

    public Material myMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHp;
        shield = maxShiled;
        myMaterial = shiledObject.GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP: " + hp + "/" + maxHp;
        shieldText.text = "Shield: " + shield + "/" + maxShiled;
        //hpBar.offsetMax = new Vector2(hp/maxHp, hpBar.offsetMax.y);
        SetAlpha(shield/200);
        hpBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slider/maxHp);

    }

        public void SetAlpha(float alphaValue)
    {
        
        // 1. Get the current color
        Color color = myMaterial.color;

        // 2. Update the alpha (value between 0.0f and 1.0f)
        color.a = Mathf.Clamp01(alphaValue);

        // 3. Assign the modified color back to the material
        myMaterial.color = color;
    }

}
