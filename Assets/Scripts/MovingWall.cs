using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float moveDistance = 10f;
    public float waitTime = 0.5f;
    public bool reverseStart = false;

    private Vector3 moveDirection;
    private float movedDistance = 0f;
    private bool isWaiting = false;

    void Start()
    {
        moveDirection = reverseStart ? Vector3.left : Vector3.right;
    }

    void Update()
    {
        if (!isWaiting)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            transform.Translate(moveDirection * moveStep);
            movedDistance += moveStep;

            if (movedDistance >= moveDistance)
            {
                moveDirection = -moveDirection;
                movedDistance = 0f;
                isWaiting = true;
                Invoke(nameof(ResumeMovement), waitTime);
            }
        }
    }

    void ResumeMovement()
    {
        isWaiting = false;
    }

    public Vector3 GetWallMovement()
    {
        return moveDirection * moveSpeed * Time.deltaTime;
    }

}
