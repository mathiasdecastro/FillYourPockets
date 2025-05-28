using UnityEngine;

[System.Serializable]
public class TurretData
{
    public Vector2 position;

    public TurretData(Vector2 position) => this.position = position;
}

public class TowerController : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 20f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private TurnManager turnManager;

    private bool _hasAttacked;
    private Vector2 _attackDirection;
    private Transform _player;

    private static readonly Vector2[] AttackOffsets = {
        new(1, 0.5f),
        new(-1, -0.5f),
        new(1, -0.5f),
        new(-1, 0.5f),
        new(2, 1),
        new(2, -1),
        new(-2, 1),
        new(-2, -1),
    };

    private void Start() => _player = GameObject.Find("Player").transform;

    private void Update()
    {
        if (turnManager.stage == StageType.TurretAttack && !_hasAttacked && !turnManager.isGameOver)
        {
            if (IsPlayerInRange())
            {
                ShootArrow();
                _hasAttacked = true;
            }

            turnManager.EndTurn();
        }

        if (turnManager.stage != StageType.TurretAttack)
            _hasAttacked = false;
    }

    private bool IsPlayerInRange()
    {
        Vector2 playerPos = _player.position;
        Vector2 pos = transform.position;

        foreach (var offset in AttackOffsets)
        {
            if (playerPos == pos + offset)
            {
                _attackDirection = offset;
                return true;
            }
        }

        return false;
    }

    private void ShootArrow()
    {
        var arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        var rb = arrow.GetComponent<Rigidbody2D>();

        if (rb)
        {
            var normalizedDirection = _attackDirection.normalized;
            rb.linearVelocity = normalizedDirection * arrowSpeed;
        }
    }
}
