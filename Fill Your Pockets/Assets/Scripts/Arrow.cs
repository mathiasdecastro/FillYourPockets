using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 25;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision avec : " + collision.gameObject.name);
        EnnemyHealth ennemy = collision.collider.GetComponent<EnnemyHealth>();

        if (ennemy != null)
            ennemy.TakeDamage(damage);

        Destroy(gameObject);
    }
}

