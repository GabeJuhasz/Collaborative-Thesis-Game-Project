using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class load : MonoBehaviour
{
    // 仅用于 Unity 编辑器测试 / Only used when testing in the Unity Editor.
    public bool resetProgressForTesting = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void loadScene()
    {
#if UNITY_EDITOR
        if (resetProgressForTesting)
        {
            string[] levels = { "Forest level", "Desert level", "Magma level", "snow level", "boss planet" };
            foreach (string level in levels)
                PlayerPrefs.DeleteKey("PlanetRoute.Completed." + level);

            PlayerPrefs.Save();
        }
#endif
        SceneManager.LoadScene("Planet Select");
    }

    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
