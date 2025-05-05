using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public float moveSpeed = 5f;

    private bool isMoving = false;
    private Vector2 targetPos;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 input = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.W))
                input = new Vector2(1, 0.5f);
            else if (Input.GetKeyDown(KeyCode.S))
                input = new Vector2(-1, -0.5f);
            else if (Input.GetKeyDown(KeyCode.D))
                input = new Vector2(1, -0.5f);
            else if (Input.GetKeyDown(KeyCode.A))
                input = new Vector2(-1, 0.5f);

            if (input != Vector2.zero)
            {
                targetPos = (Vector2)transform.position + input;
                StartCoroutine(MoteToTarget(targetPos));
            }
        }
    }

    private IEnumerator MoteToTarget(Vector2 dest)
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