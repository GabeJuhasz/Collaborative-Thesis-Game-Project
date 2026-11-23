using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Author: Ruiqi Pi
// Purpose: Manage planet selection, locked colors, and the level exit trigger.
public class PlanetManager : MonoBehaviour
{
    [Serializable]
    public class PlanetColor
    {
        public Renderer planetRenderer;
        public Material grayscaleMaterial;

        [NonSerialized] public Material originalMaterial;
        [NonSerialized] public MaterialPropertyBlock originalProperties;
    }

    [Serializable]
    public class Stop
    {
        public string sceneName;
        public Button button;
        public Text nameLabel;
        public Text status;
        public GameObject fog;
        public Graphic[] connection = new Graphic[0];
        public PlanetColor[] colors = new PlanetColor[0];
    }

    [Header("Planet selection / 选关")]
    public Stop[] stops = new Stop[0];

    [Header("Level exit / 关卡终点")]
    public Transform player;
    public string selectScene = "Planet Select";

    // 沿用旧版存档键 / Keep the existing progress keys.
    const string SaveKey = "PlanetRoute.Completed.";
    static event Action ProgressChanged;
    private bool triggered;

    void Awake()
    {
        foreach (Stop stop in stops)
        {
            if (stop == null) continue;

            // 先保存原色，再应用锁定状态 / Cache original colors before locking.
            foreach (PlanetColor color in stop.colors)
            {
                if (color == null || color.planetRenderer == null) continue;
                color.originalMaterial = color.planetRenderer.sharedMaterial;
                color.originalProperties = new MaterialPropertyBlock();
                color.planetRenderer.GetPropertyBlock(color.originalProperties);
            }

            if (stop.button != null)
                stop.button.onClick.AddListener(() => OpenLevel(stop));
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

        // 关闭管理器时恢复外观 / Restore appearance when disabled.
        foreach (Stop stop in stops)
        {
            if (stop == null) continue;
            foreach (PlanetColor color in stop.colors)
                SetLockedColor(color, false);
        }
    }

    void OpenLevel(Stop stop)
    {
        if (stop.button.interactable && CanLoadScene(stop.sceneName))
            SceneManager.LoadScene(stop.sceneName);
    }

    public void Refresh()
    {
        bool available = true;

        foreach (Stop stop in stops)
        {
            if (stop == null) continue;
            bool completed = available && PlayerPrefs.GetInt(SaveKey + stop.sceneName, 0) == 1;

            if (stop.button != null) stop.button.interactable = available;
            if (stop.nameLabel != null) stop.nameLabel.enabled = available;
            if (stop.fog != null) stop.fog.SetActive(!available);
            if (stop.status != null)
            {
                stop.status.text = completed ? "COMPLETED" : available ? "AVAILABLE" : "LOCKED";
                stop.status.color = available ? Color.white : Color.gray;
            }

            foreach (PlanetColor color in stop.colors)
                SetLockedColor(color, !available);

            foreach (Graphic link in stop.connection)
                if (link != null)
                    link.color = completed ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1);

            // 前一关完成，下一关才开放 / Unlock the next stop only after completion.
            available = completed;
        }
    }

    void SetLockedColor(PlanetColor color, bool locked)
    {
        if (color == null || color.planetRenderer == null || color.originalProperties == null)
            return;

        Renderer renderer = color.planetRenderer;
        renderer.sharedMaterial = locked && color.grayscaleMaterial != null
            ? color.grayscaleMaterial : color.originalMaterial;

        if (!locked)
        {
            renderer.SetPropertyBlock(color.originalProperties);
            return;
        }

        // 只改变当前渲染器，不修改材质文件 / Change this renderer, not material assets.
        Material material = renderer.sharedMaterial;
        var tint = new MaterialPropertyBlock();
        if (material != null)
        {
            if (material.HasProperty("_Color"))
            {
                Color original = material.GetColor("_Color");
                tint.SetColor("_Color", new Color(original.r * 0.65f, original.g * 0.65f,
                    original.b * 0.65f, original.a));
            }
            if (material.HasProperty("_EmissionColor"))
                tint.SetColor("_EmissionColor", Color.black);
        }
        renderer.SetPropertyBlock(tint);
    }

    void OnTriggerEnter(Collider other)
    {
        // 此脚本与终点 Collider 挂在同一物体 / Put this script on the exit collider object.
        if (triggered || player == null || !other.transform.IsChildOf(player))
            return;
        if (!CanLoadScene(selectScene)) return;

        triggered = true;
        CompleteLevel(gameObject.scene.name);
        SceneManager.LoadScene(selectScene);
    }

    public static void CompleteLevel(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName)) return;
        PlayerPrefs.SetInt(SaveKey + sceneName, 1);
        PlayerPrefs.Save();
        ProgressChanged?.Invoke();
    }

    bool CanLoadScene(string sceneName)
    {
        if (!string.IsNullOrWhiteSpace(sceneName) && Application.CanStreamedLevelBeLoaded(sceneName))
            return true;

        Debug.LogWarning("Scene is not available in the build scene list: " + sceneName, this);
        return false;
    }

    // 仅用于重新测试路线 / Reset only this route for testing.
    [ContextMenu("Reset Route Progress")]
    public void ResetProgress()
    {
        foreach (Stop stop in stops)
            if (stop != null && !string.IsNullOrWhiteSpace(stop.sceneName))
                PlayerPrefs.DeleteKey(SaveKey + stop.sceneName);

        PlayerPrefs.Save();
        ProgressChanged?.Invoke();
    }
}
