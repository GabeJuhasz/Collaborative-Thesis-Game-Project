using UnityEngine;
using UnityEngine.SceneManagement;

// Author: Ruiqi Pi
// Purpose: Restart the current level or return to planet selection.
public class FailMenu : MonoBehaviour
{
    public string selectScene = "Planet Select";

    // 重新开始当前关卡 / Restart the current level
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 返回选关 / Return to planet selection
    public void ReturnToSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(selectScene);
    }
}
