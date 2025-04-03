using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private float mouseSensitivity = 200f;
    public float yRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
