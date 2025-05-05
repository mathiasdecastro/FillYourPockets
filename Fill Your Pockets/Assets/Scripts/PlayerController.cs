using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public float moveSpeed = 5f;
    public float arrowSpeed = 20f;
    public GameObject arrowPrefab;
    public Transform shootPoint;
     
    private bool isMoving = false;
    private Vector2 targetPos;
    private Vector2 moveDirection;
    private Animator animator;

    void Start()
    {
        targetPos = transform.position;
        moveDirection = new Vector2(1, -0.5f);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving)
            HandleMovementInput();
        
        if (Input.GetKeyDown(KeyCode.Space))
            HandleShootingInput();
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

    private IEnumerator MoveToTarget(Vector2 dest)
    {
        isMoving = true;

        while ((Vector2)transform.position != dest)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);
           
            yield return null;
        }

        transform.position = dest;
        isMoving = false;
    }
}