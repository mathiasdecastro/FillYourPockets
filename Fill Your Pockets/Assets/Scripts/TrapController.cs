using UnityEngine;

[System.Serializable]
public class TrapData
{
    public Vector2 position;

    public TrapData(Vector2 position) => this.position = position;
}

public class TrapController : MonoBehaviour
{
    [SerializeField] private float damage = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerCombat>();
            player.TakeDamage(damage);
        }
        else if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyCombat>();
            enemy.TakeDamage(damage);
        }
    }
}
