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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Shoot");
            isAttacking = true;
        }
        
        if (Input.GetKeyDown(KeyCode.F))
            HandleAttackInput();
    
        if (Input.GetKeyDown(KeyCode.E))
            animator.SetTrigger("Potion");
    }

    public void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            
        if (rb != null)
        {
            Vector2 isoDirection = new Vector2(moveDirection.x, moveDirection.y).normalized;
            rb.linearVelocity = isoDirection * arrowSpeed;
        }

        Destroy(arrow, 2f);
    }

    private void HandleAttackInput()
    {
        if (isAttacking)
            return;
        else
            StartCoroutine(Attack());
    }

    public void ThrowPotion()
    {
        targetPos = (Vector2)transform.position + moveDirection * throwDistance;
        GameObject potion = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        potion.GetComponent<Potion>().ExplodeAt(targetPos);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            Chest chest = other.GetComponent<Chest>();
            
            if (chest != null)
            {
                int collectedGold = chest.GetGold();
                AddGold(collectedGold);
                Debug.Log("Tu as récupéré : " + collectedGold);
            }
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold : " + gold);
    }
}