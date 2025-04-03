using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    private float mouseSensitivity = 200f;
    private float distanceFromPlayer = 4f;
    private float height = 4f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        //player.Rotate(Vector3.up * mouseX);

        Vector3 targetPosition = player.position - player.forward * distanceFromPlayer + Vector3.up * height;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
    }
}
