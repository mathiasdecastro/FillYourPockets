using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float damage = 30f;
    public float health = 100f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health : " + health);
        
        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCombat player = other.GetComponent<PlayerCombat>();
            player.TakeDamage(damage);
        }
    }
}