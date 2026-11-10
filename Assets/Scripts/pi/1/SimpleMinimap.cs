using UnityEngine;
using UnityEngine.UI;

// Author: Ruiqi Pi
// 用途：在二维雷达上显示两个目标的相对位置、远近和高低。 / Purpose: Show two targets on a 2D radar, using position for direction and distance, and icons for relative height.
public class SimpleMinimap : MonoBehaviour
{
    public Transform player;          // 玩家 / Player
    public Transform target1;         // 地图上的第一个物体 / First target in the scene
    public Transform target2;         // 地图上的第二个物体 / Second target in the scene

    public Image icon1;               // 雷达里的第一个图标 / First target's radar icon
    public Image icon2;               // 雷达里的第二个图标 / Second target's radar icon

    public Sprite aboveIcon;          // ▲ 高于玩家 / Above the player
    public Sprite belowIcon;          // ▼ 低于玩家 / Below the player
    public Sprite levelIcon;          // ● 与玩家高度接近 / Near the player's height

    public float mapScale = 0.1f;      // 世界距离转换为雷达距离的比例 / Radar UI units per world unit
    public float heightTolerance = 5f; // 高度差在这个范围内算同高 / Height difference treated as the same level

    public RectTransform playerArrow;
    public RectTransform enemyArrow1;
    public RectTransform enemyArrow2;



    void Update()
    {
        ShowTarget(target1, icon1);
        ShowTarget(target2, icon2);

        RotateArrow(player, playerArrow);
        RotateArrow(target1, enemyArrow1);
        RotateArrow(target2, enemyArrow2);
    }

    void ShowTarget(Transform target, Image icon)
    {
        if (player == null || target == null || icon == null)
            return;

        // 目标相对于玩家的位置。 / Get the target's position relative to the player.
        Vector3 offset = target.position - player.position;

        // 世界 X、Z 对应雷达的左右、上下，不随玩家转向。 / Map world X and Z to radar X and Y; the map does not rotate with the player.
        icon.rectTransform.anchoredPosition =
            new Vector2(offset.x, offset.z) * mapScale;

        // 世界里的 Y 差值单独用来判断高度。 / Use the world Y difference to choose the height icon.
        if (offset.y > heightTolerance)
            icon.sprite = aboveIcon;
        else if (offset.y < -heightTolerance)
            icon.sprite = belowIcon;
        else
            icon.sprite = levelIcon;



    }

    void RotateArrow(Transform ship, RectTransform arrow)
    {
        if (ship == null || arrow == null) 
            return;
        Vector3 forward = ship.forward;

        if(forward.x * forward.x +forward.z * forward.z < 0.0001f)
            return;
        float angle = Mathf.Atan2 (forward.x, forward.z) * Mathf.Rad2Deg;
        arrow.localRotation = Quaternion.Euler(0f,0f,-angle);
    }
}
