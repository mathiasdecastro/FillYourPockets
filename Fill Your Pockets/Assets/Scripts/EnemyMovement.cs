using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public TurnManager tm;
    public Tilemap tilemap;
    public List<Sprite> nonWalkableTiles = new List<Sprite>();
    public Vector2[] directions = new Vector2[]
    {
        new Vector2(1, 0.5f),
        new Vector2(-1, -0.5f),
        new Vector2(1, -0.5f),
        new Vector2(-1, 0.5f),
        new Vector2(2, 0),
        new Vector2(-2, 0),
        new Vector2(0, 1),
        new Vector2(0, -1)
    };

    private bool isMoving = false;
    private bool hasMoved = false;
    private Transform target;
    private Vector2 moveTarget;
    private List<Vector2> blockedPositions = new List<Vector2>();

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        GameObject[] fences = GameObject.FindGameObjectsWithTag("Fence");

        foreach (GameObject fence in fences)
        {
            Vector2 fencePos = (Vector2)fence.transform.position;
            blockedPositions.Add(fencePos);
        }
    }

    void Update()
    {
        if ((tm.stage == StageType.EnemyMoveFirst || tm.stage == StageType.EnemyMoveSecond) && !hasMoved && !tm.isGameOver)
        {
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
                tm.EndTurn();
            }
        }
    }

    void MoveToTarget()
    {
        Vector2 targetPos = target.position;
        Vector2 currentPos = transform.position;
        Vector2 bestMove = currentPos;

        float minDist = Vector2.Distance(currentPos, targetPos);

        foreach (Vector2 dir in directions)
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