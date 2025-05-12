using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float health = 100f;
    public float arrowSpeed = 20f;
    public float throwingDistance = 3f;
    public GameObject arrowPrefab;
    public GameObject potionPrefab;
    public Transform shootPoint;
    public TurnManager tm;

    private Animator animator;
    private Vector2 direction;

    void Start()
    {
        animator = GetComponent<Animator>();
        direction = new Vector2(1, -0.5f);
    }

    void Update()
    {
        if (tm.stage == StageType.PlayerAttack && !tm.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Shoot");
                tm.EndTurn();
            }
                

            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Potion");
                tm.EndTurn();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            Destroy(gameObject);
    }

    public void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            
        if (rb != null)
        {
            Vector2 isoDirection = new Vector2(direction.x, direction.y).normalized;
            rb.linearVelocity = isoDirection * arrowSpeed;
        }

        Destroy(arrow, 2f);
    }

    public void ThrowPotion()
    {
        Vector2 targetPos = (Vector2)transform.position + direction * throwingDistance;
        GameObject potion = Instantiate(potionPrefab, transform.position, Quaternion.identity);
        
        potion.GetComponent<Potion>().ExplodeAt(targetPos);
    }
}