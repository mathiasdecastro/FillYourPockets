using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            
            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}