using UnityEngine;

public class Arrow : MonoBehaviour
{
    private const float Damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyCombat>();

            if (enemy)
                enemy.TakeDamage(Damage);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerCombat>();

            if (player)
                player.TakeDamage(Damage);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Fence"))
            Destroy(gameObject);
    }
}