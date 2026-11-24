using UnityEngine;
using TMPro;

// Author: Ruiqi Pi
// Purpose: Display the player's heading as a direction and angle.
public class ShipCompass : MonoBehaviour
{
    public Transform player;
    public TMP_Text compassText;

    private string[] directions =
        { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        compassText = this.gameObject.GetComponent<TMP_Text>();
    }

    void LateUpdate()
    {
        if (player == null || compassText == null)
            return;

        // 读取机头的水平朝向 / Read the ship's horizontal heading
        Vector3 forward = player.forward;

        // 机头竖直时保留上次显示 / Keep the last heading when vertical
        if (forward.x * forward.x + forward.z * forward.z < 0.0001f)
            return;

        float angle = Mathf.Atan2(forward.x, forward.z) * Mathf.Rad2Deg;
        angle = Mathf.Repeat(angle, 360f);

        int index = Mathf.RoundToInt(angle / 45f) % 8;
        int degrees = Mathf.RoundToInt(angle) % 360;

        compassText.text = directions[index] + " " + degrees.ToString("000") + "°";
    }
}
