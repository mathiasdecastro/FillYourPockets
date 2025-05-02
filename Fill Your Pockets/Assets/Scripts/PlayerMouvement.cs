using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public float mouvementSpeed = 5f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveY) * mouvementSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
