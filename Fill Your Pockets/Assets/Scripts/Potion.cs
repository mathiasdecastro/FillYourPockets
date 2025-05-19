using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float delayBeforeExplode = 0.5f;
    public float damage = 50f;
    public LayerMask damageLayer;

    private Animator animator;
    private List<Vector2> blockedPositions = new List<Vector2>();

    public void Awake()
    {
        animator = GetComponent<Animator>();
        GameObject[] fences = GameObject.FindGameObjectsWithTag("Fence");

        foreach (GameObject fence in fences)
        {
            Vector2 fencePos = (Vector2)fence.transform.position;
            blockedPositions.Add(fencePos);
        }
    }

    public void ExplodeAt(Vector2 targetPosition)
    {
        if (blockedPositions.Contains(targetPosition))
            return;

        transform.position = targetPosition;
        StartCoroutine(Explode(targetPosition));
    }

    private IEnumerator Explode(Vector2 target)
    {
        yield return new WaitForSeconds(delayBeforeExplode);

        animator.SetTrigger("bomb");
        Debug.Log("Explosion");
        float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength / 2);
        
        DoExplosionDamage(target);
        Destroy(gameObject);
    }

    void DoExplosionDamage(Vector2 center)
    {
        List<Vector2> affectedTiles = GetAdjacentTiles(center);

        foreach (Vector2 pos in affectedTiles)
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(pos, damageLayer);

            foreach (Collider2D hit in hits)
            {
                EnemyCombat enemy = hit.GetComponent<EnemyCombat>();

                if (enemy != null)
                    enemy.TakeDamage((int)damage);
            }
        }
    }

    List<Vector2> GetAdjacentTiles(Vector2 center)
    {
        float tileW = 1f;
        float tileH = 0.5f;

        return new List<Vector2>()
        {
            center,
            center + new Vector2(tileW, 0),
            center + new Vector2(-tileW, 0),
            center + new Vector2(0, tileH),
            center + new Vector2(0, -tileH)
        };
    }
}
