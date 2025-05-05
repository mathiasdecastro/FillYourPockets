using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float cellWidth = 1f;
    public float cellHeight = 0.45f;

    private Vector3 moveDirection;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!isMoving)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (moveX != 0 || moveY != 0)
            {
                moveDirection = new Vector3(moveX, moveY, 0).normalized;

                targetPosition = new Vector3(
                    Mathf.Round(transform.position.x + moveDirection.x * cellWidth),
                    Mathf.Round(transform.position.y + moveDirection.y * cellHeight),
                    transform.position.z
                );

                isMoving = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
                isMoving = false;
        }

       
    }
}