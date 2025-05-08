using UnityEngine;

public class TrapController : MonoBehaviour
{
    public float damage = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCombat player = other.GetComponent<PlayerCombat>();
            player.TakeDamage(damage);
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyCombat enemy = other.GetComponent<EnemyCombat>();
            enemy.TakeDamage(damage);
        }
    }
}
