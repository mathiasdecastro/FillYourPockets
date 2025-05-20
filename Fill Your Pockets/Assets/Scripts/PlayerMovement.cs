using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private bool hasMoved = false;
    private Animator animator;
    private Vector2 targetPosition;

    protected override void Start()
    {
        base.Start();
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (turnManager.stage == StageType.PlayerMoveFirst || turnManager.stage == StageType.PlayerMoveSecond && !turnManager.isGameOver)
        {
            if ((Vector2)transform.position == targetPosition)
            {
                if (hasMoved)
                {
                    turnManager.EndTurn();
                    hasMoved = false;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 nextPos = ConvertToGrid(mousePos);

                    if (IsAdjacent(targetPosition, nextPos) && IsWalkable(mousePos) && !blockedPositions.Contains(nextPos))
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
}