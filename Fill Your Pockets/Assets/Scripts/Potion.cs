using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private float delayBeforeExplosion = 0.5f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private LayerMask damageLayer;

    private static readonly int Bomb = Animator.StringToHash("bomb");
    private readonly List<Vector2> _blockedPositions = new();
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        var fences = GameObject.FindGameObjectsWithTag("Fence");

        foreach (var fence in fences)
        {
            Vector2 fencePos = fence.transform.position;
            _blockedPositions.Add(fencePos);
        }
    }

    public void ExplodeAt(Vector2 targetPosition)
    {
        if (_blockedPositions.Contains(targetPosition))
            return;

        transform.position = targetPosition;
        StartCoroutine(Explode(targetPosition));
    }

    private IEnumerator Explode(Vector2 target)
    {
        yield return new WaitForSeconds(delayBeforeExplosion);

        _animator.SetTrigger(Bomb);
        var animLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength / 2);
        
        DoExplosionDamage(target);
        Destroy(gameObject);
    }

    private void DoExplosionDamage(Vector2 center)
    {
        var affectedTiles = GetAdjacentTiles(center);

        foreach (var pos in affectedTiles)
        {
            var hits = Physics2D.OverlapPointAll(pos, damageLayer);

            foreach (var hit in hits)
            {
                var enemy = hit.GetComponent<EnemyCombat>();

                if (enemy)
                    enemy.TakeDamage(damage);
            }
        }
    }

    private List<Vector2> GetAdjacentTiles(Vector2 center)
    {
        const float tileW = 1f;
        const float tileH = 0.5f;

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
