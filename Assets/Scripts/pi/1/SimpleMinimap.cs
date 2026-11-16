using UnityEngine;
using UnityEngine.UI;

// Author: Ruiqi Pi
// 在二维雷达上显示玩家朝向、敌机位置、机头和高低。 / Shows player heading, enemy positions, headings and relative height on a 2D radar.
public class SimpleMinimap : MonoBehaviour
{
    public Transform player;
    public RectTransform playerArrow;

    public Image arrowTemplate;
    public Image heightTemplate;
    public Sprite aboveIcon, belowIcon, levelIcon;

    public float mapScale = 0.1f;
    public float heightTolerance = 5f;
    public float iconGap = 12f;

    void LateUpdate()
    {
        if (player == null) return;

        if (playerArrow != null) PointArrow(playerArrow, player);
        if (arrowTemplate == null || heightTemplate == null) return;

        foreach (MinimapEnemy enemy in MinimapEnemy.ActiveEnemies)
        {
            DrawEnemy(enemy);
        }
    }

    void DrawEnemy(MinimapEnemy enemy)
    {
        // 首次显示时复制一对图标。 / Create one pair of icons when the enemy first appears.
        if (enemy.arrow == null)
        {
            enemy.arrow = CopyIcon(arrowTemplate);
            enemy.heightIcon = CopyIcon(heightTemplate);
        }

        Vector3 offset = enemy.transform.position - player.position;
        Vector2 point = new Vector2(offset.x, offset.z) * mapScale;
        enemy.arrow.rectTransform.anchoredPosition = point;
        PointArrow(enemy.arrow.rectTransform, enemy.transform);

        // 高低符号单独摆放，始终保持正立。 / Position the height symbol separately so it stays upright.
        Vector2 side;
        if (offset.y > heightTolerance)
        {
            enemy.heightIcon.sprite = aboveIcon;
            side = Vector2.up;
        }
        else if (offset.y < -heightTolerance)
        {
            enemy.heightIcon.sprite = belowIcon;
            side = Vector2.down;
        }
        else
        {
            enemy.heightIcon.sprite = levelIcon;
            side = Vector2.right;
        }

        enemy.heightIcon.rectTransform.anchoredPosition = point + side * iconGap;
    }

    Image CopyIcon(Image template)
    {
        Image icon = Instantiate(template, transform, false);
        icon.gameObject.SetActive(true);
        return icon;
    }

    void PointArrow(RectTransform arrow, Transform ship)
    {
        Vector3 nose = ship.forward;

        // 机头竖直时保留上次朝向。 / Keep the last heading when the nose points straight up or down.
        if (nose.x * nose.x + nose.z * nose.z < 0.0001f) return;

        float angle = -Mathf.Atan2(nose.x, nose.z) * Mathf.Rad2Deg;
        arrow.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
