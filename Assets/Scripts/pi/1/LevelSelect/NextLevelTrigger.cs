using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Ruiqi Pi
// Purpose: Complete the current level and return to planet selection.
public class NextLevelTrigger : MonoBehaviour
{
    public Transform player; // 玩家根物体 / Player root
    public string selectScene = "Planet Select";

    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        // 只允许玩家触发一次 / Only the player can trigger this once
        if (triggered || player == null ||
            !other.transform.IsChildOf(player))
            return;

        triggered = true;

        // 保存通关记录 / Save level completion
        PlanetRoute.CompleteLevel(SceneManager.GetActiveScene().name);

        // 返回选关场景 / Return to planet selection
        SceneManager.LoadScene(selectScene);
    }
}
