using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
// Author: Ruiqi Pi
// Purpose: Move and rotate a spaceship in six degrees of freedom using keyboard and mouse controls.
public class SimpleShipFlight : MonoBehaviour
{
    [Min(0f)] public float moveSpeed = 25f;
    [Min(0.01f)] public float moveAcceleration = 50f;
    [Min(0f)] public float turnSpeed = 90f;
    [Min(0.01f)] public float turnAcceleration = 360f;
    [Min(0f)] public float mouseSensitivity = 0.1f;
    [Range(0f, 0.9f)] public float mouseDeadZone = 0.05f;

    private Rigidbody body;
    private Vector3 moveInput;
    private Vector3 turnInput;
    private Vector2 mouseStick;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = false;
        body.useGravity = false;
        body.linearDamping = 0f;
        body.angularDamping = 0f;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Update()
    {
        moveInput = Vector3.zero;
        turnInput = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Escape) || !Application.isFocused)
        {
            ReleaseMouse();
            return;
        }

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            mouseStick = Vector2.zero;
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            return;
        }

        if (Input.GetKey(KeyCode.W)) moveInput.z += 1f;
        if (Input.GetKey(KeyCode.S)) moveInput.z -= 1f;
        if (Input.GetKey(KeyCode.D)) moveInput.x += 1f;
        if (Input.GetKey(KeyCode.A)) moveInput.x -= 1f;
        if (Input.GetKey(KeyCode.Space)) moveInput.y += 1f;
        if (Input.GetKey(KeyCode.LeftShift)) moveInput.y -= 1f;
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);

        mouseStick += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;
        mouseStick = Vector2.ClampMagnitude(mouseStick, 1f);
        if (Input.GetMouseButtonDown(2)) mouseStick = Vector2.zero;

        Vector2 steering = mouseStick.magnitude > mouseDeadZone ? mouseStick : Vector2.zero;
        turnInput.x = -steering.y;
        turnInput.y = steering.x;
        if (Input.GetKey(KeyCode.Q)) turnInput.z += 1f;
        if (Input.GetKey(KeyCode.E)) turnInput.z -= 1f;
        turnInput = Vector3.ClampMagnitude(turnInput, 1f);
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = body.rotation * moveInput * moveSpeed;
        Vector3 acceleration = (targetVelocity - body.linearVelocity) / Time.fixedDeltaTime;
        body.AddForce(Vector3.ClampMagnitude(acceleration, moveAcceleration), ForceMode.Acceleration);

        Vector3 targetAngularVelocity = body.rotation * turnInput * turnSpeed * Mathf.Deg2Rad;
        Vector3 angularAcceleration = (targetAngularVelocity - body.angularVelocity) / Time.fixedDeltaTime;
        body.AddTorque(Vector3.ClampMagnitude(angularAcceleration, turnAcceleration * Mathf.Deg2Rad), ForceMode.Acceleration);
    }

    void ReleaseMouse()
    {
        moveInput = Vector3.zero;
        turnInput = Vector3.zero;
        mouseStick = Vector2.zero;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) ReleaseMouse();
    }

    void OnDisable()
    {
        ReleaseMouse();
        if (body != null)
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }

    void OnGUI()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            GUI.Label(new Rect(10f, 10f, 460f, 24f), "Click to fly | Esc: release mouse | Middle mouse: center");
            return;
        }

        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 marker = center + new Vector2(mouseStick.x, -mouseStick.y) * 100f;
        GUI.Label(new Rect(center.x - 5f, center.y - 10f, 20f, 24f), "+");
        GUI.Label(new Rect(marker.x - 5f, marker.y - 10f, 20f, 24f), "o");
    }
}
