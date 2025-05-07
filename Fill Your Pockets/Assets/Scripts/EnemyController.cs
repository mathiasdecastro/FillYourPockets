using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health : " + health);
        
        if (health <= 0)
        {
            Debug.Log("Enemy eliminated");
            Destroy(gameObject);
        }
    }
}
