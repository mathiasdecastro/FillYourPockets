using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyCombat enemy = other.GetComponent<EnemyCombat>();

            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            PlayerCombat player = other.GetComponent<PlayerCombat>();

            if (player != null)
                player.TakeDamage(damage);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Fence"))
            Destroy(gameObject);
    }
}