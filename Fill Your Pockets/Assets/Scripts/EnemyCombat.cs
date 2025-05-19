using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float damage = 30f;
    public float health = 100f;
    public TurnManager tm;

    private bool hasAttacked = false;
    private Animator animator;
    private EnemyMovement em;

    void Start()
    {
        animator = GetComponent<Animator>();
        em = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (tm.stage == StageType.EnemyAttack && !hasAttacked && !tm.isGameOver)
        {
            if (IsPlayerAround())
            {
                AttackPlayer();
                hasAttacked = true;
            }

            tm.EndTurn();
        }

        if (tm.stage != StageType.EnemyAttack)
            hasAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health : " + health);
        
        if (health <= 0)
            Destroy(gameObject);
    }

    bool IsPlayerAround()
    {
        Vector2 pos = (Vector2)transform.position;
        Vector2 playerPos = GameObject.Find("Player").transform.position;

        foreach (Vector2 dir in em.directions)
        {
            if (playerPos == pos + dir)
                return true;
        }

        return false;
    }

    void AttackPlayer()
    {
        PlayerCombat player = GameObject.Find("Player").GetComponent<PlayerCombat>();
        player.TakeDamage(damage);
    }
}