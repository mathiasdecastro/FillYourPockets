using System.Linq;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float damage = 30f;
    [SerializeField] private TurnManager turnManager;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    
    private bool _hasAttacked;
    private Animator _animator;

    public float health = 100f;
    
    private void Start() => _animator = GetComponent<Animator>();

    private void Update()
    {
        if (turnManager.stage == StageType.EnemyAttack && !_hasAttacked && !turnManager.isGameOver)
        {
            if (IsPlayerAround())
            {
                var player = GameObject.Find("Player").GetComponent<PlayerCombat>();
                player.TakeDamage(damage);
                _hasAttacked = true;
            }

            turnManager.EndTurn();
        }

        if (turnManager.stage != StageType.EnemyAttack)
            _hasAttacked = false;
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        _animator.SetTrigger(Hit);
        
        if (health <= 0)
            Destroy(gameObject);
    }

    private bool IsPlayerAround()
    {
        Vector2 pos = transform.position;
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        Debug.Log(Directions.Isometric.Any(dir => playerPos == pos + dir));
        return Directions.Isometric.Any(dir => playerPos == pos + dir);
    }
}