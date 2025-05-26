using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    private bool isMoving = false;
    private bool hasMoved = false;
    private Transform target;
    private Vector2 moveTarget;

    public Animator animator;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if ((turnManager.stage == StageType.EnemyMoveFirst || turnManager.stage == StageType.EnemyMoveSecond) && !hasMoved && !turnManager.isGameOver)
        {
            animator.SetBool("Walking", true);
            MoveToTarget();
            hasMoved = true;
        }

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);

            if ((Vector2)transform.position == moveTarget)
            {
                isMoving = false;
                hasMoved = false;
                animator.SetBool("Walking", false);
                turnManager.EndTurn();
            }
        }
    }

    void MoveToTarget()
    {
        Vector2 targetPos = target.position;
        Vector2 currentPos = transform.position;
        Vector2 bestMove = currentPos;

        float minDist = Vector2.Distance(currentPos, targetPos);

        foreach (Vector2 dir in Directions.Isometric)
        {
            Vector2 nextMove = currentPos + dir;

            if (blockedPositions.Contains(nextMove))
                continue;

            float dist = Vector2.Distance(nextMove, targetPos);

            if (dist < minDist)
            {
                minDist = dist;
                bestMove = nextMove;
            }
        }

        moveTarget = bestMove;
        isMoving = true;
    }
}