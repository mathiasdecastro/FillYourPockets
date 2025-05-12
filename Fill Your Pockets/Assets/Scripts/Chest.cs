using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int goldPerInterval = 5;
    public float intervalSeconds = 3f;
    public float interactionDistance = 1.5f; // Distance pour interagir avec le coffre

    private float timer = 0f;
    private int storeGold = 0;
    private Animator animator;

    private GameObject player;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= intervalSeconds)
        {
            storeGold += goldPerInterval;
            timer = 0f;
        }

        if (!isOpened && Input.GetKeyDown(KeyCode.F))
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= interactionDistance)
            {
                GetGold();
                isOpened = true;
            }
        }
    }

    public int GetGold()
    {
        int goldToGive = storeGold;
        animator.SetTrigger("Open");
        StartCoroutine(DestroyAfterAnimation());
        animator.SetBool("Empty", true);
        return goldToGive;
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1.0f); 
        Destroy(gameObject);
    }

    public int getStoreGold()
    {
        return storeGold;
    }
}
