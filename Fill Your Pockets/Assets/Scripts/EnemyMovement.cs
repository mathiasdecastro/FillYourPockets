using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    private bool _isMoving;
    private bool _hasMoved;
    private Transform _target;
    private Vector2 _moveTarget;
    private Animator _animator;
    
    protected override void Start()
    {
        base.Start();
        _target = GameObject.FindWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if ((turnManager.stage == StageType.EnemyMoveFirst || turnManager.stage == StageType.EnemyMoveSecond) && !_hasMoved && !turnManager.isGameOver)
        {
            _animator.SetBool("Walking", true);
            MoveToTarget();
            _hasMoved = true;
        }

        if (_isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _moveTarget, moveSpeed * Time.deltaTime);

            if ((Vector2)transform.position == _moveTarget)
            {
                _isMoving = false;
                _hasMoved = false;
                _animator.SetBool("Walking", false);
                turnManager.EndTurn();
            }
        }
    }

    void MoveToTarget()
    {
        Vector2 targetPos = _target.position;
        Vector2 currentPos = transform.position;
        Vector2 bestMove = currentPos;

        float minDist = Vector2.Distance(currentPos, targetPos);

        foreach (Vector2 dir in Directions.Isometric)
        {
            Vector2 nextMove = currentPos + dir;

            if (BlockedPositions.Contains(nextMove))
                continue;

            float dist = Vector2.Distance(nextMove, targetPos);

            if (dist < minDist)
            {
                minDist = dist;
                bestMove = nextMove;
            }
        }

        _moveTarget = bestMove;
        _isMoving = true;
    }
}