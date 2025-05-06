using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public float moveSpeed = 5f;
    public float arrowSpeed = 20f;
    public float attackRange = 1f;
    public float attackDuration = 0.5f;
    public float throwDistance = 2f;
    public int gold = 0;
    public GameObject arrowPrefab;
    public GameObject hitboxPrefab;
    public GameObject bombPrefab;
    public Transform shootPoint;
     
    private bool isMoving = false;
    private bool isAttacking = false;
    private Vector2 targetPos;
    private Vector2 moveDirection;
    private Animator animator;
    private GameObject currentHitbox;

    void Start()
    {
        targetPos = transform.position;
        moveDirection = new Vector2(1, -0.5f);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving)
        {
            animator.SetBool("Walk", false);
            HandleMovementInput();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Shoot");
            HandleShootingInput();
        }
        if (Input.GetKeyDown(KeyCode.F))
            HandleAttackInput();
        
        if (Input.GetKeyDown(KeyCode.E))
            HandleThrowInput();
    }

    private void HandleMovementInput()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            input = new Vector2(1, 0.5f);
            moveDirection = new Vector2(1, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            input = new Vector2(-1, -0.5f);
            moveDirection = new Vector2(-1, -0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            input = new Vector2(1, -0.5f);
            moveDirection = new Vector2(1, -0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            input = new Vector2(-1, 0.5f);
            moveDirection = new Vector2(-1, 0.5f);
        }

        if (input != Vector2.zero)
        {
            targetPos = (Vector2)transform.position + input;
            StartCoroutine(MoveToTarget(targetPos));
        }
    }

    private void HandleShootingInput()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            
        if (rb != null)
        {
            Vector2 isoDirection = new Vector2(moveDirection.x, moveDirection.y).normalized;
            rb.linearVelocity = isoDirection * arrowSpeed;
        }

        Destroy(arrow, 5f);
    }

    private void HandleAttackInput()
    {
        if (isAttacking)
            return;
        else
            StartCoroutine(Attack());
    }

    private void HandleThrowInput()
    {
        targetPos = (Vector2)transform.position + moveDirection * throwDistance;
        
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.GetComponent<Bomb>().ExplodeAt(targetPos);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        currentHitbox = Instantiate(hitboxPrefab, transform.position, Quaternion.identity);
        currentHitbox.transform.position = transform.position + transform.right * attackRange;
        
        yield return new WaitForSeconds(attackDuration);
        
        Destroy(currentHitbox);
       
        isAttacking = false;
    }

    private IEnumerator MoveToTarget(Vector2 dest)
    {
        isMoving = true;
        animator.SetBool("Walk", true);

        while ((Vector2)transform.position != dest)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);
           
            yield return null;
        }

        transform.position = dest;
        animator.SetBool("Walk", false);
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit !");
            other.GetComponent<EnemyController>().TakeDamage(10);
        }

        Chest chest = other.GetComponent<Chest>();
        
        if (chest != null)
        {
            int collectedGold = chest.GetGold();
            AddGold(collectedGold);
            Debug.Log("Tu as récupéré : " + collectedGold);
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold : " + gold);
    }
}