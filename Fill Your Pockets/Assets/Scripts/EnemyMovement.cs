using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public float moveCooldown = 3f;

    private float timer = 0f;
    private Transform target;
    private Vector2[] directions = new Vector2[]
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


    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= moveCooldown)
        {
            MoveToTarget();
            timer = 0f;
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
            float dist = Vector2.Distance(nextMove, targetPos);

            if (dist < minDist)
            {
                minDist = dist;
                bestMove = nextMove;
            }
        }

        transform.position = bestMove;
    }
}