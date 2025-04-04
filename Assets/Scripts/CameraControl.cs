using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float distanceFromPlayer = 4f;
    public float height = 4f;
    public float verticalRotationLimit = 30f;
    public float mouseSensitivity = 5f;

    private float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Get vertical mouse movement and clamp it
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation - mouseY, -verticalRotationLimit, verticalRotationLimit);

        // Get the player's Y-axis rotation (horizontal rotation)
        Quaternion horizontalRotation = Quaternion.Euler(0f, player.eulerAngles.y, 0f);

        // Apply only vertical tilt separately
        Quaternion verticalRotationQuat = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Calculate offset using horizontal rotation first, then apply vertical tilt
        Vector3 offset = horizontalRotation * new Vector3(0, 0, -distanceFromPlayer);
        offset = verticalRotationQuat * offset;
        offset.y += height; // Ensure height stays constant

        // Update camera position and look at the player
        transform.position = player.position + offset;
        transform.LookAt(player.position + Vector3.up * height * 0.5f);
    }
}
