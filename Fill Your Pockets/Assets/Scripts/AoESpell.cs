using UnityEngine;
using UnityEngine.InputSystem;

public class AoESpell : MonoBehaviour
{
    public float radius = 3f;
    public float damage = 50f;
    public LayerMask enemyLayer;
    public GameObject explosionEffect;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            CastSpell();
    }

    public void CastSpell()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnnemyHealth eh = enemy.GetComponent<EnnemyHealth>();

            if (eh != null)
                eh.TakeDamage((int) damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
