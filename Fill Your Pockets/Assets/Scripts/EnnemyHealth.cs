using NUnit.Framework;
using UnityEngine;

public class EnnemyHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Ennemie touché ! Santé restante : " + health);

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Ennemie éliminé");
        Destroy(gameObject);
    }
}
