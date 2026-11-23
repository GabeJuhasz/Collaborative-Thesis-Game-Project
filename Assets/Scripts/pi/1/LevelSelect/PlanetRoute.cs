using System;
using UnityEngine;
using UnityEngine.UI;

public class PlanetRoute : MonoBehaviour
{
    [Serializable]
    public class Stop
    {
        public string sceneName;
        public GameObject planet;
        public Button button;
        public Text nameLabel;
        public Text status;
        public GameObject fog;
        public Graphic[] connection;
    }

    public Menuing navigation;
    public Stop[] stops;

    const string SaveKey = "PlanetRoute.Completed.";
    static event Action ProgressChanged;

    void Awake()
    {
        foreach (Stop stop in stops)
        {
            Stop destination = stop;
            stop.button.onClick.AddListener
            (() =>
            {
                if (destination.button.interactable)
                    navigation.GoToScene(destination.sceneName);
            }
            );
        }
    }

    void OnEnable()
    {
        ProgressChanged += Refresh;
        Refresh();
    }

    void OnDisable()
    {
        ProgressChanged -= Refresh;
    }

    // 工程师在真正通关时调用，进入或退出关卡不会解锁。
    public static void CompleteLevel(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName)) return;
        PlayerPrefs.SetInt(SaveKey + sceneName, 1);
        PlayerPrefs.Save();
        ProgressChanged?.Invoke();
    }

    public void Refresh()
    {
        if (stops == null) return;
        bool available = true;
        foreach (Stop stop in stops)
        {
            bool completed = available && PlayerPrefs.GetInt(SaveKey + stop.sceneName, 0) == 1;
            stop.button.interactable = available;
            if (stop.nameLabel != null) stop.nameLabel.enabled = available;
            if (stop.fog != null) stop.fog.SetActive(!available);
            stop.status.text = completed ? "COMPLETED" : available ? "AVAILABLE" : "LOCKED";
            stop.status.color = available ? Color.white : Color.gray;

            // 只调整这个场景里的显示，不修改共用材质。
            foreach (Renderer renderer in stop.planet.GetComponentsInChildren<Renderer>())
            {
                var tint = new MaterialPropertyBlock();
                Material material = renderer.sharedMaterial;
                if (!available && material != null)
                {
                    if (material.HasProperty("_Color"))
                    {
                        Color color = material.GetColor("_Color");
                        tint.SetColor("_Color", new Color(color.r * 0.65f, color.g * 0.65f, color.b * 0.65f, color.a));
                    }
                    if (material.HasProperty("_EmissionColor"))
                        tint.SetColor("_EmissionColor", Color.black);
                }
                renderer.SetPropertyBlock(tint);
            }

            if (stop.connection != null)
                foreach (Graphic link in stop.connection)
                    link.color = completed ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1);
            available = completed;
        }
    }

    // 仅清除这条路线的进度，供编辑器里重新测试。
    [ContextMenu("Reset Route Progress")]
    public void ResetProgress()
    {
        foreach (Stop stop in stops) PlayerPrefs.DeleteKey(SaveKey + stop.sceneName);
        PlayerPrefs.Save();
        ProgressChanged?.Invoke();
    }
}
