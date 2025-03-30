using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 16f;
    public bool reverseStart = false;

    private Vector3 moveDirection;
    private float movedDistance = 0f;

    void Start()
    {
        moveDirection = reverseStart ? Vector3.back : Vector3.forward;
    }

    void Update()
    {
        // Move the platform
        float moveStep = moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection * moveStep);
        movedDistance += moveStep;

        // Reverse direction when movedDistance reaches moveDistance
        if (movedDistance >= moveDistance)
        {
            moveDirection = -moveDirection;
            movedDistance = 0f;
        }
    }

    public Vector3 GetPlatformMovement()
    {
        return moveDirection * moveSpeed * Time.deltaTime;
    }
}
