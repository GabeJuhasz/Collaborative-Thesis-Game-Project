using UnityEngine;

// 用途：星球被迷雾锁定时显示黑白材质，解锁时恢复原来的彩色材质。
// Shows a grayscale material while the planet is locked and restores the original on unlock.
public class LockedPlanetColor : MonoBehaviour
{
    public Renderer planetRenderer;
    public Material grayscaleMaterial;

    private Material originalMaterial;

    void OnEnable()
    {
        if (planetRenderer == null || grayscaleMaterial == null) return;

        originalMaterial = planetRenderer.sharedMaterial;
        planetRenderer.sharedMaterial = grayscaleMaterial;
    }

    void OnDisable()
    {
        if (planetRenderer == null || originalMaterial == null) return;

        planetRenderer.sharedMaterial = originalMaterial;
        originalMaterial = null;
    }
}
