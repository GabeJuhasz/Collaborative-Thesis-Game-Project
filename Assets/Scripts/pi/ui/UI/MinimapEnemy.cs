using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author: Ruiqi Pi
// 登记敌机并清理它的雷达图标。 / Registers this enemy and removes its radar icons.
public class MinimapEnemy : MonoBehaviour
{
    internal static readonly List<MinimapEnemy> ActiveEnemies = new List<MinimapEnemy>();

    internal Image arrow;
    internal Image heightIcon;

    void OnEnable()
    {
        ActiveEnemies.Add(this);
    }

    void OnDisable()
    {
        ActiveEnemies.Remove(this);

        if (arrow != null) Destroy(arrow.gameObject);
        if (heightIcon != null) Destroy(heightIcon.gameObject);

        arrow = null;
        heightIcon = null;
    }
}
