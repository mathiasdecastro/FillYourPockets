using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Tower Settings")]
    public float damage = 1f;
    public float arrowSpeed = 20f;
    public GameObject arrowPrefab;
    public Transform shootPoint;

    [Header("References")]
    public TurnManager turnManager;

    private bool hasAttacked = false;
    private Animator animator;
    private Vector2 attackDirection;
    private Transform player;

    private static readonly Vector2[] attackOffsets = new Vector2[]
    {
        new Vector2(1, 0.5f),
        new Vector2(-1, -0.5f),
        new Vector2(1, -0.5f),
        new Vector2(-1, 0.5f),
        new Vector2(2, 0),
        new Vector2(-2, 0),
        new Vector2(0, 1),
        new Vector2(0, -1),
        new Vector2(2, 1),
        new Vector2(2, -1),
        new Vector2(-2, 1),
        new Vector2(-2, -1),
        new Vector2(4, 0),
        new Vector2(-4, 0),
        new Vector2(0, 2),
        new Vector2(0, -2),
        new Vector2(3, -0.5f),
        new Vector2(-3, 0.5f),
        new Vector2(1, 1.5f),
        new Vector2(-1, 1.5f),
        new Vector2(1, -1.5f),
        new Vector2(-1, -1.5f),
    };

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (turnManager.stage == StageType.TurretAttack && !hasAttacked && !turnManager.isGameOver)
        {
            if (IsPlayerInRange())
            {
                ShootArrow();
                hasAttacked = true;
            }

            turnManager.EndTurn();
        }

        if (turnManager.stage != StageType.TurretAttack)
            hasAttacked = false;
    }

    /// <summary>
    /// Checks if the player is within one of the defined attack directions.
    /// </summary>
    /// <returns>True if the player is in range, false otherwise.</returns>
    bool IsPlayerInRange()
    {
        Vector2 playerPos = player.position;
        Vector2 pos = (Vector2)transform.position;

        foreach (Vector2 offset in attackOffsets)
        {
            if (playerPos == pos + offset)
            {
                attackDirection = offset;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Instantiates and shoots an arrow in the stored attack direction.
    /// </summary>
    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
    
        if (rb != null)
        {
            Vector2 normalizedDirection = attackDirection.normalized;
            rb.linearVelocity = normalizedDirection * arrowSpeed;
        }
    }
}
