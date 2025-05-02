using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public float mouvementSpeed = 5f;
    public TurnManager turnManager;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (turnManager.isGameOver == false)
        {
            Vector3 move = new Vector3(moveX, 0, moveY) * mouvementSpeed * Time.deltaTime;
            transform.Translate(move, Space.World);
        }

        //turnManager.EndTurn();
    }
}
