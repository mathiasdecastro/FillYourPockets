using UnityEngine;

[System.Serializable]
public class TrapData
{
    public Vector2 position;

    public TrapData(Vector2 position) => this.position = position;
}

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
