using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject stickPrefab;
    public float attackRange = 1f;
    public int damage = 35;
    public LayerMask enemyLayer;
    public float stickDuration = 0.3f;
    public Transform stickSpawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            MeleeHit();
    }

    void MeleeHit()
    {

        GameObject stick = Instantiate(stickPrefab, stickSpawnPoint.position, stickSpawnPoint.rotation);
        Destroy(stick, stickDuration);

        Collider[] hitEnemis = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemyCollider in hitEnemis)
        {
            EnnemyHealth enemy = enemyCollider.GetComponent<EnnemyHealth>();

            if (enemy != null)
            {
                Debug.Log("Coup de batôn sur : " + enemy.name);
                enemy.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
