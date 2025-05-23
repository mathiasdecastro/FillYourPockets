using UnityEngine;
using TMPro;
public class PlayerCombat : MonoBehaviour
{
    public float health = 100f;
    public float arrowSpeed = 20f;
    public float throwingDistance = 3f;
    public float stickDamage = 10f;
    public GameObject arrowPrefab;
    public GameObject potionPrefab;
    public Transform shootPoint;
    public TurnManager tm;

    private TextMeshProUGUI healthText;
    private Animator animator;
    private Vector2 direction;

    void Start()
    {
        animator = GetComponent<Animator>();
        direction = new Vector2(1, -0.5f);
        healthText = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();
        healthText.text = health.ToString();
    }

    public void ShootArrowButton()
    {
        if (tm.stage == StageType.PlayerAttack && !tm.isGameOver)
        {
            animator.SetTrigger("Shoot");
            tm.EndTurn();
        }
    }

    public void ThrowPotionButton()
    {
        if (tm.stage == StageType.PlayerAttack && !tm.isGameOver)
        {
            animator.SetTrigger("Potion");
            tm.EndTurn();
        }
    }

    public void StickAttackButton()
    {
        if (tm.stage == StageType.PlayerAttack && !tm.isGameOver)
        {
            animator.SetTrigger("Stick");
            tm.EndTurn();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Je prend des degats");
        healthText.text = health.ToString();

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

    public void StickAttck()
    {
        Vector2 enemyPos = GameObject.FindWithTag("Enemy").transform.position;
        Vector2 pos = (Vector2)transform.position;

        foreach (Vector2 dir in Directions.Isometric)
        {
            if (enemyPos == pos + dir)
            {
                GameObject.FindWithTag("Enemy").GetComponent<EnemyCombat>().TakeDamage(stickDamage);
                break;
            }
        }
    }
}