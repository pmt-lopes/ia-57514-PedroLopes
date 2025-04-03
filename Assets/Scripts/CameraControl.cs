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
        Vector3 offset = player.rotation * new Vector3(0, height, -distanceFromPlayer);
        transform.position = player.position + offset;

        transform.LookAt(player.position + Vector3.up * height * 0.5f);
    }
}
