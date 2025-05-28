using System;
using System.Globalization;
using System.Linq;
using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    public float health = 100f;
    
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private TurnManager turnManager;

    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Potion = Animator.StringToHash("Potion");
    private static readonly int Stick = Animator.StringToHash("Stick");
    
    private const float StickDamage = 10f;
    private const float ArrowSpeed = 20f;
    private const float ThrowingDistance = 3f;

    private enum PendingAction { None, Shoot, Potion, Stick }
    
    private bool _waitingForDirection;
    private PendingAction _pendingAction;
    private TextMeshProUGUI _healthText;
    private Animator _animator;
    private Vector2 _direction;
    private Camera _camera;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _direction = new Vector2(1, -0.5f);
        _healthText = GameObject.Find("HPText").GetComponent<TextMeshProUGUI>();
        _healthText.text = health.ToString(CultureInfo.InvariantCulture);
        _camera = Camera.main;
    }

    private void Update() 
    {
        if (Input.GetMouseButton(0)) DetectTileClick();
    }

    private void DetectTileClick()
    {
        if (!_waitingForDirection) return;
        
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        var newDirection = PlayerMovement.ConvertToGrid(mouseWorldPos);
        var playerPos = (Vector2)transform.position;
        var clickedDirection = newDirection - playerPos;

        if (clickedDirection.x < 0)
            _direction = clickedDirection.y < 0 ? new Vector2(-1, -0.5f) : new Vector2(-1, 0.5f);
        else
            _direction = clickedDirection.y < 0 ? new Vector2(1, -0.5f) : new Vector2(1, 0.5f);
        
        ExecutePendingAction();
    }
    
    public void ShootArrowButton()
    {
        if (turnManager.stage != StageType.PlayerAttack || turnManager.isGameOver) return;
        
        _waitingForDirection = true;
        _pendingAction = PendingAction.Shoot;
    }

    public void ThrowPotionButton()
    {
        if (turnManager.stage != StageType.PlayerAttack || turnManager.isGameOver) return;
        
        _waitingForDirection = true;
        _pendingAction = PendingAction.Potion;
    }

    public void StickAttackButton()
    {
        if (turnManager.stage != StageType.PlayerAttack || turnManager.isGameOver) return;
       
        _waitingForDirection = true;
        _pendingAction = PendingAction.Stick;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        _healthText.text = health.ToString(CultureInfo.InvariantCulture);
        _animator.SetTrigger("Hit");
        if (health <= 0) Destroy(gameObject);
    }

    public void ShootArrow()
    {
        var arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        var rb = arrow.GetComponent<Rigidbody2D>();

        if (rb)
        {
            var isoDirection = new Vector2(_direction.x, _direction.y).normalized;
            rb.linearVelocity = isoDirection * ArrowSpeed;
        }

        Destroy(arrow, 2f);
    }

    public void ThrowPotion()
    {
        var targetPos = (Vector2)transform.position + _direction * ThrowingDistance;
        var potion = Instantiate(potionPrefab, transform.position, Quaternion.identity);

        potion.GetComponent<Potion>().ExplodeAt(targetPos);
    }

    public void StickAttack()
    {
        Vector2 enemyPos = GameObject.FindWithTag("Enemy").transform.position;
        var pos = (Vector2)transform.position;

        if (Directions.Isometric.Any(dir => enemyPos == pos + dir))
            GameObject.FindWithTag("Enemy").GetComponent<EnemyCombat>().TakeDamage(StickDamage);
    }

    private void ExecutePendingAction()
    {
        switch (_pendingAction)
        {
            case PendingAction.Stick:
                _animator.SetTrigger(Stick);
                break;
            
            case PendingAction.Potion:
                _animator.SetTrigger(Potion);
                break;
            
            case PendingAction.Shoot:
                _animator.SetTrigger(Shoot);
                break;
            
            case PendingAction.None:
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _waitingForDirection = false;
        _pendingAction = PendingAction.None;
        turnManager.EndTurn();
    }
}