using UnityEngine;
using TMPro;

//Author: peter
// show the misssion point ant its distance on the hud

public class MissionMarkerHUD : MonoBehaviour
{

    public Camera playerCamera;
    public Transform player;
    public Transform missionPoint;

    public RectTransform marker;
    public TMP_Text distanceText;

    void LateUpdate()
    {
        if(marker == null)return;

        if (playerCamera == null || player == null || missionPoint == null)
        {
            marker.gameObject.SetActive(false);
            return;
        }

        Vector3 point = playerCamera.WorldToScreenPoint(missionPoint.position);

        bool visible = point.z > 0f && playerCamera.pixelRect.Contains(point);
        marker.gameObject.SetActive(visible);
        if (!visible) return;

        marker.position = new Vector3(point.x, point.y, 0f);

        if (distanceText != null) 
        {
            float distance = Vector3.Distance(player.position,missionPoint.position);
            distanceText.text = Mathf.RoundToInt(distance) + "m";
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
