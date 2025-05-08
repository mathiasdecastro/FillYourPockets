using UnityEngine;

public class TowerController : MonoBehaviour
{
    public float damage = 1f;
    public float arrowSpeed = 20f;
    public GameObject arrowPrefab;
    public Transform shootPoint;

    private Animator animator;
    private Vector2 direction;
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
        direction = new Vector2(1, -0.5f);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsPlayerInRange())    
            ShootArrow();
    }

    bool IsPlayerInRange()
    {
        Vector2 playerPos = GameObject.Find("Player").transform.position;

        foreach (Vector2 dir in directions)
        {
            if (playerPos == (Vector2)transform.position + dir)
            {
                direction = dir;
                return true;
            }
        }

        return false;
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
    
        if (rb != null)
        {
            Vector2 isoDirection = new Vector2(direction.x, direction.y).normalized;
            rb.linearVelocity = isoDirection * arrowSpeed;
        }
    }
}
