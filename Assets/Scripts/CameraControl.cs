using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float distanceFromPlayer = 4f;
    public float height = 4f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Only use the player's Y-axis rotation to control the camera's position
        Quaternion playerRotation = Quaternion.Euler(0f, player.eulerAngles.y, 0f);
        Vector3 offset = playerRotation * new Vector3(0, height, -distanceFromPlayer);
        transform.position = player.position + offset;

        transform.LookAt(player.position + Vector3.up * height * 0.5f);
    }
}
