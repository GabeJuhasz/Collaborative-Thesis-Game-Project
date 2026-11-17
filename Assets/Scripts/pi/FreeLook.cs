using UnityEngine;

//Author : peter
//let the player look around with the mouse


public class FreeLook : MonoBehaviour
{

    public float sensitivity = 2f;

    private float yaw;

    private float pitch;
    void Start()
    {
        yaw = transform.localScale.y;
        pitch = Mathf.DeltaAngle(0f, transform.localEulerAngles.x);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
