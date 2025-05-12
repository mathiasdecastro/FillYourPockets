using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public TurnManager tm;

    private bool hasMoved = false;
    private Vector2 targetPosition;
    private Animator animator;

    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (tm.stage == StageType.PlayerMoveFirst || tm.stage == StageType.PlayerMoveSecond && !tm.isGameOver)
        {
            if ((Vector2)transform.position == targetPosition)
            {
                if (hasMoved)
                {
                    tm.EndTurn();
                    hasMoved = false;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 nextPos = ConvertToGrid(mousePos);

                    if (IsAdjacent(targetPosition, nextPos))
                    {
                        animator.SetBool("Walk", true);
                        targetPosition = nextPos;
                        hasMoved = true;
                    }
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            animator.SetBool("Walk", false);
        }
    }

    private Vector2 ConvertToGrid(Vector2 input)
    {
        float x = Mathf.Round(input.x);
        float y = Mathf.Round(input.y);

        if ((int)x % 2 == 0)
            y += 0.5f;
        else
            y += 1f;
 
        return new Vector2(x, y);
    }

    private bool IsAdjacent(Vector2 current, Vector2 target)
    {
        float dx = target.x - current.x;
        float dy = target.y - current.y;

        if ((dx == 1 && dy == 0.5f) || 
            (dx == -1 && dy == -0.5f) || 
            (dx == 1 && dy == -0.5f) || 
             (dx == -1 && dy == 0.5f))
            return true;

        if ((dx == -2 && dy == 0) || 
            (dx == 0 && dy == -1) || 
            (dx == 0 && dy == 1) || 
            (dx == 2 && dy == 0))
            return true;

        return false;
    }
}